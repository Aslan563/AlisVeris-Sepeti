using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IStoreRepository
{
    IQueryable<Product> Products { get; }
    IQueryable<Categories> Category { get; }
    void CreateProduct(Product entity);

   
    IEnumerable<Product> GetProducts(int Page, string category, string search, int Pagesize);

}