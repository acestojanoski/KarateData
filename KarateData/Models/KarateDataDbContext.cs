using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KarateData.Models
{
    public class KarateDataDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Competitor> Competitors { get; set; }

        public KarateDataDbContext() : base("DefaultConection")
        {
            Database.SetInitializer<KarateDataDbContext>(new CreateDatabaseIfNotExists<KarateDataDbContext>());
        }
    }
}