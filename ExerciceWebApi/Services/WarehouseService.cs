using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;

namespace ExerciceWebApi.Services
{
    public class WarehouseService : IBaseCrudService<Warehouse>
    {
        private readonly ExerciceWebApi.DbContext Context;
        public WarehouseService(ExerciceWebApi.DbContext _context)
        {
            Context = _context;
        }

        public async Task<Warehouse> Create(Warehouse entity)
        {
            entity.WarehouseId = Guid.NewGuid().ToString();
            Context.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var dbWarehouse = await Context.Warehouses.FindAsync(id);
            if (dbWarehouse == null)
            {
                return false;
            }

            Context.Remove(dbWarehouse);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Warehouse>> GetAll()
        {
			return await Context.Warehouses.Include(c => c.Storages).ToListAsync();
        }

        public Task<Warehouse> GetById(string id)
        {
            return null;
        }

        public async Task<Warehouse> Update(string id, Warehouse entity)
        {
            var dbWarehouse = await Context.Warehouses.Include(c => c.Storages).FirstOrDefaultAsync(x=> x.WarehouseId == id);
            if (dbWarehouse != null)
            {
                dbWarehouse.WarehouseName = entity.WarehouseName;
                dbWarehouse.WarehouseAddress = entity.WarehouseAddress;
                dbWarehouse.Storages = entity.Storages;

                await Context.SaveChangesAsync();

            }

            return dbWarehouse;
        }
    }
}