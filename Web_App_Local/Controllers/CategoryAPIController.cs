using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local.Controllers
{
    [Route("api/[controller]")]
  //  [Authorize]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly IRepository<Category, int> catRepo;

        public CategoryAPIController(IRepository<Category, int> catRepo)
        {
            this.catRepo = catRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cats = await catRepo.GetAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cats = await catRepo.GetAsync(id);
            return Ok(cats);
        }

        [HttpPost]
   //     [HttpPost("{CategoryId}/{CategoryName}/{BasePrice}")]
       // public async Task<IActionResult> Post([FromRoute]Category cat)
        public async Task<IActionResult> Post(Category cat)

        {
            //try
            //{

                if (ModelState.IsValid)
                {
                    if (cat.BasePrice < 2000)
                     throw new Exception("Salary Validation Failed");
                    
                     var cats = await catRepo.CreateAsync(cat);
                    return Ok(cats);
                }
                return BadRequest();
            //}
            //catch(Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put( int id, [FromBody] Category cat)
        {
            if (ModelState.IsValid)
            {
                var cats = await catRepo.UpdateAsync(id, cat);
                return Ok(cats);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cats = await catRepo.DeleteAsync(id);
            return Ok(cats);
        }
    }
}