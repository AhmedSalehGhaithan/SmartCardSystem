using SmartCardLibrary1.Model;
namespace SmartCardLibrary1.SmartCardRepository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserByNameAsync(string username);
    }
}
