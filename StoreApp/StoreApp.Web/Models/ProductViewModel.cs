namespace StoreApp.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; }=string.Empty;
    public string Category { get; set; } = string.Empty;
}
public class ProductListViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    public Pageinfo pageinfo=new();
}

public class Pageinfo{
    public int Totalitems {get;set;}
    public int page {get;set;}
    public int Totalpage =>(int) Math.Ceiling((decimal)Totalitems/page );
    public int CurrentPage {get;set;}
}