using SmartCardSystem.SmartCardResponses;
namespace SmartCardSystem.SmartCardRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<CrudResponse> AddAsync(T entity);
        Task<CrudResponse> UpdateAsync(T entity);
        Task<CrudResponse> DeleteAsync(int id);
    }
}
