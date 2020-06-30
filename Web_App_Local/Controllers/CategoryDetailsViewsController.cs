using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_App_Local.Models;
using Web_App_Local.ViewModels;

namespace Web_App_Local.Controllers
{
    public class CategoryDetailsViewsController : Controller
    {
        private readonly AppJune2020DbContext _context;

        public CategoryDetailsViewsController(AppJune2020DbContext context)
        {
            _context = context;
        }

        // GET: CategoryDetailsViews
        public async Task<IActionResult> Index (int CategoryRowId)
        {
            if (CategoryRowId == 0)
            {
                //[FromBody]Category category    
                var tables = new CategoryDetailsView
                {
                    Categories = await _context.Categories.ToListAsync(),
                    Products = await _context.Products.ToListAsync()
                };
                return View(tables);
            }
            else
            {
                var tables = new CategoryDetailsView
                {
                    Categories = await _context.Categories.ToListAsync(),
                    Products = _context.Products.Where(i => (i.CategoryRowId== CategoryRowId)).ToList()
                };
                return View(tables);

            }
            // return View(await _context.CategoryDetailsView.ToListAsync());
        }

        // GET: CategoryDetailsViews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoryDetailsView = await _context.CategoryDetailsView
        //        .FirstOrDefaultAsync(m => m.categoryRowId == id);
        //    if (categoryDetailsView == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoryDetailsView);
        //}

        // GET: CategoryDetailsViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryDetailsViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryRowId")] CategoryDetailsView categoryDetailsView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryDetailsView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDetailsView);
        }

        // GET: CategoryDetailsViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryDetailsView = await _context.CategoryDetailsView.FindAsync(id);
            if (categoryDetailsView == null)
            {
                return NotFound();
            }
            return View(categoryDetailsView);
        }

        // POST: CategoryDetailsViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //   [HttpPost]
        //  [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("categoryRowId")] CategoryDetailsView categoryDetailsView)
        //{
        //    if (id != categoryDetailsView.categoryRowId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(categoryDetailsView);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CategoryDetailsViewExists(categoryDetailsView.categoryRowId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(categoryDetailsView);
        //}

        // GET: CategoryDetailsViews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoryDetailsView = await _context.CategoryDetailsView
        //        .FirstOrDefaultAsync(m => m.categoryRowId == id);
        //    if (categoryDetailsView == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoryDetailsView);
        //}

        // POST: CategoryDetailsViews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var categoryDetailsView = await _context.CategoryDetailsView.FindAsync(id);
        //    _context.CategoryDetailsView.Remove(categoryDetailsView);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CategoryDetailsViewExists(int id)
        //{
        //    return _context.CategoryDetailsView.Any(e => e.categoryRowId == id);
        //}
        //}
    }
}
