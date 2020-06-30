using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_App_Local.Models;

namespace Web_App_Local.Services
{
    public class CategoryRepository : IRepository<Category, int>
    {
        private readonly AppJune2020DbContext ctx;
        public CategoryRepository(AppJune2020DbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task <Category> CreateAsync(Category entity)
        {
            var Result = await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return Result.Entity;
        }

      public async  Task<bool> DeleteAsync(int id)
        {
            var Cat = await ctx.Categories.FindAsync(id);
            if (Cat != null)
            {
                ctx.Categories.Remove(Cat);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;

        }

      public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await ctx.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var Cat = await ctx.Categories.FindAsync(id);
            if (Cat != null)
            {
                Cat.CategoryId = entity.CategoryId;
                Cat.CategoryName = entity.CategoryName;
                Cat.BasePrice = entity.BasePrice;
                await ctx.SaveChangesAsync();
            }
                return Cat;

        }
    }
}
