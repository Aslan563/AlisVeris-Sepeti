using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Data.Concrete;

namespace StoreApp.Data.Concrete
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public List<OrderItem> orderItems { get; set; } = new();     

    }


    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order order { get; set; }=null!;
        public int ProductId { get; set; }
        public Product Product { get; set; }=null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}

