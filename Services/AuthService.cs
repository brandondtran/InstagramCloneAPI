using InstagramCloneAPI.Dtos;
using InstagramCloneAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace InstagramCloneAPI.Services
{
    public class AuthService(ApplicationDbContext context) : IAuthService
    {
        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> LoginAsync(LoginDto loginRequest)
        {
     
        }

        public async Task LogoutAsync()
        {
            // Implement your logout logic here
            // For example, invalidate the user's token

            await Task.CompletedTask;
        }
    }
}
