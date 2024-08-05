using SmartCardSystem.SmartCardDTO;
using SmartCardSystem.SmartCardResponses;
namespace SmartCardSystem.SmartCardRepository
{
    public interface IPaymentRepository : IRepository<Transaction>
    {
        Task<CrudResponse> SendMoneyAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionByAccountIdAsync(string accountId);
        Task<CrudResponse> DepositAsync(DepositMoney depositMoney);
        Task<List<DepositMoney>> GetDepositTransactionAsync();
        Task<CrudResponse> DeleteDepositAsync(int id);
        Task<List<DepositMoney>> GetDepositByAccountNumber(string accountNumber);
        
    }
}
