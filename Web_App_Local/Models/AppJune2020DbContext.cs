using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_App_Local.ViewModels;

namespace Web_App_Local.Models
{
    public class AppJune2020DbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public AppJune2020DbContext(DbContextOptions<AppJune2020DbContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryRowId);
                    }

        public DbSet<Web_App_Local.ViewModels.CategoryDetailsView> CategoryDetailsView { get; set; }
    }
}
