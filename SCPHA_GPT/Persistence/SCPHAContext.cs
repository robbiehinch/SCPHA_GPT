using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SCPHA_GPT.Persistence
{
    public class SCPHAContext : DbContext
    {
        public DbSet<GeneratedPortAuthorityImage> Images { get; set; }
 
        public string DbPath { get; }

        public SCPHAContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "scpha.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
