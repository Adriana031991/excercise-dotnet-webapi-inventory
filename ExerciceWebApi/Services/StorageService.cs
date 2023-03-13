using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;

namespace ExerciceWebApi.Services
{
    public class StorageService : IBaseCrudService<Storage>, IStorageService
    {
        private readonly DbContext Context;
        public StorageService(DbContext _context)
        {
            Context = _context;
        }

        public async Task<Storage> Create(Storage entity)
        {
			var product = await Context.Products.FirstOrDefaultAsync(x => x.ProductId == entity.Product.ProductId);
			var warehouse = await Context.Warehouses.FirstOrDefaultAsync(x => x.WarehouseId == entity.Warehouse.WarehouseId);


			entity.StorageId = Guid.NewGuid().ToString();
            entity.LastUpdate = DateTime.Now;
            entity.Product = product;
            entity.Warehouse = warehouse;
            entity.WarehouseId = warehouse.WarehouseId;
            entity.ProductId = product.ProductId;
            
            Context.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var dbStorage = await Context.Storages.FindAsync(id);
            if (dbStorage == null)
            {
                return false;
            }

            Context.Remove(dbStorage);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Storage>> GetAll()
        {
            return await Context.Storages
                .Include(c => c.Product)
                .Include(c => c.Warehouse)
                .Include(c => c.InputOutputs)
				.ToListAsync();
        }

        public async Task<Storage> GetById(string id)
        {
            var dbStorage = await Context.Storages.Include(c => c.Product).Include(c => c.Warehouse).Include(c => c.InputOutputs).FirstOrDefaultAsync(p => p.StorageId == id);
            if (dbStorage is not null)
            {
                return dbStorage;
            }
            return null;
        }


        public async Task<Storage> Update(string id, Storage entity)
        {
			//var dbStorage = await Context.Storages.FindAsync(id);
			var dbStorage = await Context.Storages.Include(c => c.Product).Include(c => c.Warehouse).Include(c => c.InputOutputs).FirstOrDefaultAsync(p => p.StorageId == id);

			if (dbStorage != null)
            {
                dbStorage.LastUpdate = DateTime.Now;
                dbStorage.PartialQuantity = entity.PartialQuantity;
                dbStorage.ProductId = entity.Product.ProductId;
                dbStorage.WarehouseId = entity.Warehouse.WarehouseId;
                dbStorage.InputOutputs = entity.InputOutputs;

                await Context.SaveChangesAsync();

            }
            return dbStorage;


        }

        public async Task<bool> IsProductInWarehouse(string id)
        {
            var storages = await Context.Storages.Include(c => c.Product).Include(c => c.Warehouse).Include(c => c.InputOutputs).ToListAsync();

			//var storage = (from s in storages where (s.StorageId == storageId) select s);
			var storage = storages.Where(s => s.StorageId == id);

            return storage.Any();
        }

        public async Task<List<Storage?>> StorageListByWarehouse(string idWarehouse)
        {
			var storages = await Context.Storages.Include(c => c.Product).Include(c => c.Warehouse).Include(c => c.InputOutputs).ToListAsync();

			//TODO: pendiente revisar

			var a = (from p in storages
                     where p.WarehouseId == idWarehouse
                     select p).ToList();
            foreach (var b in a)
            {

                Console.WriteLine("StorageListByWarehouse " + b.ToString());
            }

            return a;

            //return products
            //		.Include(s => s.Product)
            //		.Include(s => s.Warehouse)
            //		.Where(s => s.WarehouseId == idWarehouse)
            //		.ToList();
        }
    }
}