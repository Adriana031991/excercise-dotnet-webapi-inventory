
namespace ExerciceWebApi.Services.Gateway
{
    public interface IBaseCrudService<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(string id);

        Task<T> Create(T entity);

        Task<T> Update(string id, T entity);

        Task<bool> Delete(string id);

    }
}