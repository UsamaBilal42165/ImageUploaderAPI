using ImageUploaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageUploaderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }


        public DbSet<ImageHeader> Images { get; set; }
    }
}
