using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_App_Local.Models;

namespace Web_App_Local.ViewModels
{
    public class CategoryDetailsView
    {
        [Key]
        public string Id { get; set; }
        public ICollection<Category> Categories { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
