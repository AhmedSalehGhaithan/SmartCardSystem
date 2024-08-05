using SmartCardLibrary.Model;
using SmartCardLibrary.Responses;
namespace SmartCardLibrary.SmartCardRepository
{
    public interface IPaymentRepository: IRepository<Transaction>
    {
        Task<CrudResponse> SendMoneyAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionByAccountIdAsync(string accountId);
        Task<CrudResponse> DepositAsync(DepositMoney depositMoney);
    }
}
