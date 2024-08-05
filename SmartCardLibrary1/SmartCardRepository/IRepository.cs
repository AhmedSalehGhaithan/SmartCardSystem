using SmartCardLibrary1.Responses;
namespace SmartCardLibrary1.SmartCardRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<CrudResponse> AddAsync(T entity);
        Task<CrudResponse> UpdateAsync(T entity);
        Task<CrudResponse> DeleteAsync(int id);
    }
}
