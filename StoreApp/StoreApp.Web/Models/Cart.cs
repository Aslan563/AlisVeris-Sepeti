using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models;

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public virtual void AddItem(Product product, int quantity)
    {
        var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();

        if (item == null)
        {
            Items.Add(new CartItem { Product = product, Quantity = quantity });
        }
        else
        {
            item.Quantity += quantity;
        }
    }

    public virtual void RemoveItem(Product product)
    {
        var productremove=Items.FirstOrDefault(p=>p.Product.Id==product.Id);
        if(productremove.Quantity>1){
            productremove.Quantity-=1;
        }
        else{
             Items.RemoveAll(i => i.Product.Id == product.Id);
        }
       
    }
    
    public virtual void RemoveAll(Product product)
    {
             Items.RemoveAll(i => i.Product.Id == product.Id);
       
       
    }

    public virtual double CalculateTotal()
    {
        return Items.Sum(i => i.Product.Price * i.Quantity);                                                  
    }

    public virtual void Clear()
    {
        Items.Clear();    
    }

  
}

public class CartItem
{
    public int CartItemId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}
