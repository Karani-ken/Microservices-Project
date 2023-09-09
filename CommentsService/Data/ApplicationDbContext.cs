using CommentsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Comments> Comments { get; set; }
    }
}
