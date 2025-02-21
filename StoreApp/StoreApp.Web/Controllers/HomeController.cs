using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;
public class HomeController : Controller
{
    public int Pagesize = 3;
    private readonly IStoreRepository _storeRepository;

    private readonly IMapper _IMapper;
    public HomeController(IStoreRepository storeRepository,IMapper Imapper)
    {
        _storeRepository = storeRepository;
        _IMapper=Imapper;

    }
    
    public IActionResult Index(int Page = 1, String? search="",string? category="")
    {
         
       var  Products = _storeRepository.GetProducts(Page,category??"",search??"",Pagesize).Skip((Page - 1) * Pagesize).Select(p => _IMapper.Map<ProductViewModel>(p)).Take(Pagesize);

        return View(new ProductListViewModel
        {
            Products = Products,
            pageinfo = new Pageinfo
            {
                Totalitems =_storeRepository.GetProducts(Page,category??"",search??"",Pagesize).Count(),
                page = Pagesize,
                CurrentPage = Page
            }
        });

    }


    
}