using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web_App_Local.Models
{
    public class Category
    {

        [Key]
        public int CategoryRowId { get; set; }

        [Required(ErrorMessage = "Category Id is must")]
        [StringLength(20)]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is must")]
        [StringLength(200)]
        public string CategoryName { get; set; }
    //    [NumericValidator(ErrorMessage = "Value Cannot be -ve")]
   
        [Required(ErrorMessage = "BasePrice cannot be null")]
        public int BasePrice { get; set; }

        public ICollection< Product> Products { get; set; }
    }
      



    public class Product
    {
        [Key]
        public int ProductRowId { get; set; }

        [Required(ErrorMessage = "Product Id is must")]
        [StringLength(20)]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is must")]
        [StringLength(200)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is must")]
        [StringLength(300)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is must")]
         public int Price { get; set; }

        [ForeignKey("CategoryRowId")]
        public int CategoryRowId { get; set; }

        public Category Category { get; set; }

    }

    public class CustomException
    {
        [Key]
        public int LogId { get; set; }
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string ExceptionMessage { get; set; }

        public DateTime Loggingdate { get; set; }

    }

        public class NumericValidatorAttribute : ValidationAttribute
         {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) < 0)
                {
                    return false;

                }
                return true;
            }

        }

    }
