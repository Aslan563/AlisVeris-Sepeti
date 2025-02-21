using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EFStoreRepository : IStoreRepository
{
    private StoreDbContext _context;
  


    public EFStoreRepository(StoreDbContext context)
    {
        _context = context;
    }
    public IQueryable<Product> Products => _context.Products;
     public IQueryable<Categories> Category => _context.Categories;

    public void CreateProduct(Product entity)
    {
        throw new NotImplementedException();
    }

   

    IEnumerable<Product> IStoreRepository.GetProducts(int Page, string category, string search,int Pagesize)
    {
       var query = _context.Products.AsQueryable();

        if(!string.IsNullOrEmpty(category)){
          query = query.Include(p=>p.categories).Where(p => p.categories.Any(c => c.Url == category));
        
        }
       
        if (search!=null)
        {
         query=query.Where(p=>p.Name.ToLower().Contains(search.ToLower()));
        
        }
         
        
        return query;
    }

}

