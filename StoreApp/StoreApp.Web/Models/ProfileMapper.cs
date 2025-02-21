using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<Product,ProductViewModel>();
        }
    }
}