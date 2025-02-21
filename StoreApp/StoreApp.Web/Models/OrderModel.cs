using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreApp.Web.Models
{
    public class OrderModel
    {
       
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;

        [BindNever]
        public Cart? Cart { get; set; } =new();

        public string? CartName { get; set; } = null!;
        public string? CartNumber { get; set; } = null!;
        public string? expirationMonth { get; set; } = null!;
        public string? expirationYears { get; set; } = null!;
        public string? cvc { get; set; } = null!;
    }
}