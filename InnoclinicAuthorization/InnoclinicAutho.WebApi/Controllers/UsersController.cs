using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace InnoclinicAutho.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ApiDbContext _context;

        public UsersController(
            ILogger<UsersController> logger,
            ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IActionResult> Get()
        {
            var user = new User()
            {
                DateCreated = DateTime.UtcNow,
                UserID = Guid.NewGuid(),
                IsDeleted = false,
                Email = "test@test.com",
                PasswordHash = "test"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}
