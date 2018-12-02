using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutURL.Entities;

namespace CutURL.DatabaseLayer
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base("name=UrlShortnerCS")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Statistics>()
                .HasRequired(s => s.ShortUrl)
                .WithMany(u => u.statistics)
                .Map(m => m.MapKey("shortUrl_id"));
        }

        public virtual DbSet<URLDetails> URLDetails { get; set; }
        public virtual DbSet<Statistics> Statistics { get; set; }
    }
}
