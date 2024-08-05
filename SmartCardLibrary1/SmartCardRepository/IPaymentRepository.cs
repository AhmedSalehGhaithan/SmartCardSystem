using SmartCardLibrary1.Model;
using SmartCardLibrary1.Responses;
namespace SmartCardLibrary1.SmartCardRepository
{
    public interface IPaymentRepository: IRepository<Transaction>
    {
        Task<CrudResponse> SendMoneyAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionByAccountIdAsync(string accountId);
        Task<CrudResponse> DepositAsync(DepositMoney depositMoney);
    }
}
