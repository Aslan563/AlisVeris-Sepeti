using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components
{
    public class CategoriesListViewComponent:ViewComponent
    {
        IStoreRepository _IStorerepostory;
        
        public CategoriesListViewComponent(IStoreRepository storeRepository)
        {
            _IStorerepostory=storeRepository;
        }
        public IViewComponentResult Invoke(){
           ViewBag.SelectedCategory=RouteData?.Values["category"];
            return View(_IStorerepostory.Category.Select(p=>new CategoryView{
                CategoryId=p.CategoryId,
                Name=p.Name,
                Url=p.Url
            }).ToList());
        }
    }
}