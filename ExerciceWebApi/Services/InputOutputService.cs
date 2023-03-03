
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;


namespace ExerciceWebApi.Services
{
	public class InputOutputService : IBaseCrudService<InputOutput>
	{

		private readonly DbContext Context;
		public InputOutputService(DbContext _context)
		{
			Context = _context;
		}

		public async Task<InputOutput> Create(InputOutput entity)
		{
			var storage = await Context.Storages.FirstOrDefaultAsync(x => x.StorageId == entity.StorageId);
			entity.InOutId = Guid.NewGuid().ToString();
			entity.InOutDate = DateTime.Now;
			entity.Storage = storage;
			
			Context.Add(entity);
			await Context.SaveChangesAsync();

			return entity;
		}

		public async Task<bool> Delete(string id)
		{
			var dbInputOutput = await Context.InputOutputs.FindAsync(id);
			if (dbInputOutput == null)
			{
				return false;
			}

			Context.Remove(dbInputOutput);
			await Context.SaveChangesAsync();

			return true;
		}

		public async Task<List<InputOutput>> GetAll()
		{
			return await Context.InputOutputs.Include(c => c.Storage).ToListAsync();
		}

		public Task<InputOutput> GetById(string id)
		{
			return null;
		}

		public async Task<InputOutput> Update(string id, InputOutput entity)
		{
			var dbInputOutput = await Context.InputOutputs.FindAsync(id);
			if (dbInputOutput != null)
			{
				dbInputOutput.InOutDate = entity.InOutDate;
				dbInputOutput.Quantity = entity.Quantity;
				dbInputOutput.IsInput = entity.IsInput;
				dbInputOutput.StorageId = entity.StorageId;

				await Context.SaveChangesAsync();

			}

			return dbInputOutput;
		}
	}
}