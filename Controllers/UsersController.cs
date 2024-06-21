using InstagramCloneAPI.Dtos;
using InstagramCloneAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstagramCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ApplicationDbContext context) : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }

        // // PUT: api/Users/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
        // {
        //     var user = await context.Users.FindAsync(id);
        //
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     user.Username = updateUserDto.Username;
        //     user.Email = updateUserDto.Email;
        //     user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        //
        //     context.Entry(user).State = EntityState.Modified;
        //     await context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // // DELETE: api/Users/{id}
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUser(string id)
        // {
        //     var user = await context.Users.FindAsync(id);
        //
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     context.Users.Remove(user);
        //     await context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
    }
}
