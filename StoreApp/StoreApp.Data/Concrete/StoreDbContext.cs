using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<Product>()
        .HasMany(p => p.categories)
        .WithMany(c => c.products)
        .UsingEntity<ProductCategories>(pc =>
         pc.HasKey(e => new { e.ProductId, e.CategoryId }));



        modelBuilder.Entity<Categories>()
        .HasIndex(e => e.Url)
        .IsUnique();

        modelBuilder.Entity<Categories>()
        .HasKey(c => c.CategoryId);


        modelBuilder.Entity<Product>().HasData(
            new List<Product>() {
                new() { Id=1, Name="Samsung S21", Price=17000, Description="güzel telefon",Image="~/img/21.jpg"},
                new() { Id=2, Name="Samsung S22", Price=22000, Description="güzel telefon",Image="~/img/22.jpg"},
                new() { Id=3, Name="Samsung S23", Price=25000, Description="güzel telefon",Image="~/img/23.jpg"},
                new() { Id=4, Name="Samsung S24", Price=40000, Description="güzel telefon",Image="~/img/24.jpg"},
                new() { Id=5, Name="Samsung S25", Price=60000, Description="güzel telefon",Image="~/img/25.jpg"},
                new() { Id=6, Name="Iphone 16", Price=64000, Description="güzel telefon",Image="~/img/iphone16.jpg"},
               

            }
        );

        modelBuilder.Entity<Categories>().HasData(
             new List<Categories>() {
                new() {CategoryId=1,Name="Telefon",Url="Telefon"},
                new() {CategoryId=2,Name="Elektronik",Url="Elektronik"},
                new() {CategoryId=3,Name="Beyaz Eşya",Url="Beyaz-Esya"},
             }
        );



        modelBuilder.Entity<ProductCategories>().HasData(
            new List<ProductCategories>(){
                new() {ProductId=1,CategoryId=1},
                new() {ProductId=1,CategoryId=2},
                new() {ProductId=2,CategoryId=1},
                new() {ProductId=3,CategoryId=1},
                new() {ProductId=4,CategoryId=1},
                new() {ProductId=5,CategoryId=1},
                new() {ProductId=5,CategoryId=2},
                new() {ProductId=6,CategoryId=1},
                new() {ProductId=6,CategoryId=2},

            }
        );
    }
}