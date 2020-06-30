using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_App_Local.Models;

namespace Web_App_Local.Data
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<CustomException> CustomExceptions { get; set; }
        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}