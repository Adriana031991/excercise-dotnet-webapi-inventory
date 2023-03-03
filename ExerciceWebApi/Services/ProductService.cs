using ExerciceWebApi;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;

namespace ExerciceWebApi.Services
{
    public class ProductService : IBaseCrudService<Product>
    {

        private readonly ExerciceWebApi.DbContext context;
        public ProductService(ExerciceWebApi.DbContext _context)
        {
            context = _context;
        }

        public async Task<Product> Create(Product entity)
        {
            var category = context.Categories.Single(x => x.CategoryId == entity.CategoryId);
            entity.ProductId = Guid.NewGuid().ToString();
            entity.Category = category;
            var response = context.Add(entity).Entity;
            await context.SaveChangesAsync();

            return response;

        }

        public async Task<bool> Delete(string id)
        {
            var dbProduct = await context.Products.FindAsync(id);
            if (dbProduct == null)
            {
                return false;
            }

            context.Remove(dbProduct);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            //return await context.Products.ToListAsync();
            return await context.Products.Include(p => p.Storages).ToListAsync();
        }


        public async Task<Product> GetById(string id)
        {
            var dbProduct = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            return dbProduct;
        }

        public async Task<Product> Update(string id, Product entity)
        {
            var dbProduct = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            if (dbProduct != null)
            {
                dbProduct.ProductName = entity.ProductName;
                dbProduct.CategoryId = entity.CategoryId;
                dbProduct.ProductDescription = entity.ProductDescription;
                dbProduct.TotalQuantity = entity.TotalQuantity;

                await context.SaveChangesAsync();

            }

            return dbProduct;
        }

    }
}