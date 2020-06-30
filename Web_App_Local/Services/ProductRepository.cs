using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_App_Local.Models;

namespace Web_App_Local.Services
{
    public class ProductRepository : IRepository<Product, int>
    {
        private readonly AppJune2020DbContext ctx;
        public ProductRepository(AppJune2020DbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            var Result = await ctx.Products.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return Result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Prod = await ctx.Products.FindAsync(id);
            if (Prod != null)
            {
                ctx.Products.Remove(Prod);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await ctx.Products.FindAsync(id);
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {

            var Prod = await ctx.Products.FindAsync(id);
            if (Prod != null)
            {
                Prod.ProductId = entity.ProductId;
                Prod.ProductName = entity.ProductName;
                Prod.Price = entity.Price;
                await ctx.SaveChangesAsync();
            }
            return Prod;
        }
    }
}
