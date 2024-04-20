using FootballMatchCrudApi.Matches.Model;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<FootballMatch> Matches { get; set; }
    }
}
