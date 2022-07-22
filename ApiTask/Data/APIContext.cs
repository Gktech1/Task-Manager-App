using ApiTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Data
{
    public class APIContext : IdentityDbContext<AppUser>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AppUserTask> UserTasks { get; set; }
    }
}
