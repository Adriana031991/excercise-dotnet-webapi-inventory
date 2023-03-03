
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;


namespace ExerciceWebApi.Services
{
    public class CategoryService : IBaseCrudService<Category>
    {
        private readonly ExerciceWebApi.DbContext Context;

        public CategoryService(ExerciceWebApi.DbContext _context)
        {
            Context = _context;
        }

        public async Task<Category> Create(Category entity)
        {
            entity.CategoryId = Guid.NewGuid().ToString();
            Context.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var dbCategory = await Context.Categories.FindAsync(id);
            if (dbCategory == null)
            {
                return false;
            }

            Context.Remove(dbCategory);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Category>> GetAll()
        {
            return await Context.Categories.Include(c=>c.Products).ToListAsync();
        }

        public async Task<Category> GetById(string id)
        {
            var dbCategory = await Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.CategoryId == id);
            return dbCategory;
        }

        public async Task<Category> Update(string id, Category entity)
        {
			var dbCategory = await Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.CategoryId == id);

			if (dbCategory != null)
            {
                dbCategory.CategoryName = entity.CategoryName;
                dbCategory.Products = entity.Products;


                await Context.SaveChangesAsync();
            }

            return dbCategory;
        }
    }
}