using SmartCardLibrary.Model;
namespace SmartCardLibrary.SmartCardRepository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserByNameAsync(string username);
    }
}
