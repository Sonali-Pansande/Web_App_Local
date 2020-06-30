using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category, int> catRepository;

        //private readonly AppJune2020DbContext _context;

        public CategoryController(IRepository<Category, int> catRepository)
        {
            this.catRepository = catRepository;

        }


       
        // GET: CategoryController
        public async Task <ActionResult> Index()
        {
            var cats = await catRepository.GetAsync();
            return View(cats);
        }

         
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View(new Category());
        }

        // POST: CategoryController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
           if (ModelState.IsValid)
            {
                if (category.BasePrice < 0)
                    throw new Exception("Base Price cannot ne -ve");
                category = await catRepository.CreateAsync(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var cat = await catRepository.GetAsync(id);
            return View(cat);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public  async Task< ActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category = await catRepository.UpdateAsync(id,category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var res =  await catRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

         
    }
}
