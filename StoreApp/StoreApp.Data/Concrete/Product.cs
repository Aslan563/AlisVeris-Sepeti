using System.ComponentModel.DataAnnotations;

namespace StoreApp.Data.Concrete;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string  Image { get; set; }=null!;
    public List<Categories> categories=new();

  
}
