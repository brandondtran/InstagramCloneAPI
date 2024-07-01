using InstagramCloneAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Controllers
builder.Services.AddControllers();
// Register DI Services
// builder.Services.AddScoped<IAuthService, AuthService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configure ServiceUrls settings
builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("ServiceUrls"));
var serviceUrls = builder.Configuration.GetSection("ServiceUrls").Get<ServiceUrls>();

// Register HTTP client services for external APIs
builder.Services.AddHttpClient("Keycloak", client =>
{
    if (serviceUrls != null)
    {
        client.BaseAddress = new Uri(serviceUrls.Keycloak);
    }
});

// Add db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add auth
builder.Services
    .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(options =>
    {
        // TODO: User ServiceUrls config
        options.Authority = "http://localhost:9999/realms/myrealm";
        // TODO: Need to set up proper aud
        options.Audience = "account";
        options.RequireHttpsMetadata = false;
        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            // TODO: User ServiceUrls config
            ValidIssuer = "http://localhost:9999/realms/myrealm",
            // TODO: Need to set up proper aud
            ValidAudience = "account"
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication failed. {context.Exception}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Token validated.");
                return Task.CompletedTask;
            }
        };
    });


// TODO: Temporary config to disable CORS
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

// TODO: TEMP Use CORS
app.UseCors();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Log information on startup
var logger = app.Services.GetRequiredService<ILogger<Program>>();

if (serviceUrls != null)
{
    logger.LogInformation($"Keycloak URL: {serviceUrls.Keycloak}");
}
else
{
    logger.LogCritical("ServiceUrls missing from app settings.");
    
    // Shutdown the application
    Environment.Exit(1);
}

app.Run();