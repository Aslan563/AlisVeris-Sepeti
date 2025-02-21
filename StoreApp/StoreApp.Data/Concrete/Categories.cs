using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
        public List<Product> products =new();
    }
}