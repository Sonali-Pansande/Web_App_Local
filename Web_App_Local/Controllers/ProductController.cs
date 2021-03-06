﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_App_Local.CustomSessions;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product, int> ProdRepository;
        private readonly IRepository<Category, int> CatRepository;

        public ProductController(IRepository<Product, int> ProdRepository, IRepository<Category, int> CatRepository)
        {
            this.ProdRepository = ProdRepository;
            this.CatRepository = CatRepository;
        }
         
        public ActionResult Index()
        {
            List<Product> prds = new List<Product>();

            var id = HttpContext.Session.GetInt32("CategoryRowId");
            var cat = HttpContext.Session.GetSessionData<Category>("Category");

            if (id > 0)
            {
                prds = (from p in ProdRepository.GetAsync().Result.ToList()
                        where p.CategoryRowId == id
                        select p).ToList();
            }
            else
            {
                prds =  ProdRepository.GetAsync().Result.ToList();
            }
               return View(prds);
        }


        
        public async Task<ActionResult> Create()
        {
            var prd = new Product();
            ViewBag.CategoryRowId =
                new SelectList(await CatRepository.GetAsync(), "CategoryRowId", "CategoryName");

            return View(new Product());
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Price < 0)
                    throw new Exception("Base Price cannot ne -ve");
                product = await ProdRepository.CreateAsync(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

      
        public async Task<ActionResult> Edit(int id)
        {
            var prod = await ProdRepository.GetAsync(id);
            return View(prod);
        } 

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                product = await ProdRepository.UpdateAsync(id, product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            var res = await ProdRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}
