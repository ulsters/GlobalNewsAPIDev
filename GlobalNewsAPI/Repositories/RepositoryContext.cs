using GlobalNewsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalNewsAPI.Repositories
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<UserComments> UserComments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PopularNews> PopularNews { get; set; }
        public DbSet<UserDto> UserDto { get; set; }
    }
}
