using SmartCardLibrary.Model;
namespace SmartCardLibrary.SmartCardRepository
{
    public interface IAccountRepository: IRepository<Account>
    {
        Task<Account> GetAccountByUserIdAsync(int userId);
    }
}
