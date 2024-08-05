using SmartCardLibrary1.Model;
namespace SmartCardLibrary1.SmartCardRepository
{
    public interface IAccountRepository: IRepository<Account>
    {
        Task<Account> GetAccountByUserIdAsync(int userId);
    }
}
