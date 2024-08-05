using SmartCardSystem.SmartCardDTO;
namespace SmartCardSystem.SmartCardRepository
{
    public interface IAccountRepository : IRepository<AccountDTO>
    {
        Task<AccountDTO> GetByAccountNumberAsync(string accountNumber);
    }
}
