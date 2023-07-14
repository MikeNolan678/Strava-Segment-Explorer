using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StravaSegmentExplorerDataAccess.Models;

namespace StravaSegmentExplorerUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StravaSegmentExplorerDataAccess.Models.AppUserModel>? AppUserModel { get; set; }
    }
}