using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApplication.API.Models
{
    public class Category
    {
       [Key]
       [Display(Name="Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
