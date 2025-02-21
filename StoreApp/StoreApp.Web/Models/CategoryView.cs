using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Models
{
    public class CategoryView
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}