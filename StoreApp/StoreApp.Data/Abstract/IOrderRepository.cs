using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order>  orders {get;}

        void SaveOrder(Order order);
    }
}