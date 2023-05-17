using Family.Models;
using Microsoft.EntityFrameworkCore;

namespace Family.Data
{
    public class FamilyDbContext: DbContext
    {
        public FamilyDbContext(DbContextOptions<FamilyDbContext> options)
        : base(options)
        { 
            
        }

        public DbSet<FamilyTree> familyTrees { get; set; }

    }
}
