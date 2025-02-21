using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete
{
    public class EOrderRepository : IOrderRepository
    {
        StoreDbContext _storeDbContext;
        public EOrderRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext=storeDbContext;
        }
        public IQueryable<Order> orders => throw new NotImplementedException();

        public void SaveOrder(Order order)
        {
            _storeDbContext.Orders.Add(order);
            _storeDbContext.SaveChanges();
        }
    }
}