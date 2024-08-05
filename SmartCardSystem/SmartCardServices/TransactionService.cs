using Microsoft.EntityFrameworkCore;
using SmartCardSystem.Data;
using SmartCardSystem.SmartCardDTO;
using SmartCardSystem.SmartCardRepository;
using SmartCardSystem.SmartCardResponses;
namespace SmartCardSystem.SmartCardServices
{
    public class TransactionService(HttpClient _httpClient,AppDbContext _context) : IPaymentRepository
    {
        public async Task<CrudResponse> AddAsync(Transaction entity)
        {
            if (entity == null)
                return new CrudResponse(false, "The Transactions entity cannot be null.");

            var addNewTransactions = await _context.Transactions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, "Transactions created successfully");
        }
        public async Task<CrudResponse> DeleteAsync(int id)
        {
            var check = await _context.Transactions.FirstOrDefaultAsync(_ => _.TransactionId == id);
            if (check is null)
                return new CrudResponse(false, $"Transaction with the id [{id}] is not found");
            _context.Transactions.Remove(check);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, $"Transaction Deleted Successfully");
        }
        public async Task<CrudResponse> UpdateAsync(Transaction entity)
        {
            var check = await _context.Transactions.Where(_u_ => _u_.TransactionId == entity.TransactionId).FirstOrDefaultAsync();
            if (check is null) return new CrudResponse(false, $"Transaction with id [{entity.TransactionId}] is not found");
            _context.Transactions.Update(entity);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, $"Transaction updated successfully");
        }
        public async Task<List<Transaction>> GetAllAsync() => await _context.Transactions.ToListAsync();
        public async Task<Transaction> GetByIdAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction is null) return null!;
            return transaction;
        }
        public async Task<List<Transaction>> GetTransactionByAccountIdAsync(string accountId)
        {
            try
            {
                var transactions = await _context.Transactions.Where(t => t.SenderAccountNumber == accountId).ToListAsync();
                return transactions.Any() ? transactions : new List<Transaction>();
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<CrudResponse> SendMoneyAsync(Transaction transaction)
        {
            using (var transactionOperation = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var sender = await _context.Accounts.FirstOrDefaultAsync(_sender => _sender.AccountNumber == transaction.SenderAccountNumber);
                    var receiver = await _context.Accounts.FirstOrDefaultAsync(_receiver => _receiver.AccountNumber == transaction.ReceiverAccountNumber);

                    if (sender == null || receiver == null) return new CrudResponse(false, $"The Sender and the Receiver must have an account");

                    if (sender.Balance <= 0) return new CrudResponse(false, $"Sorry,You can't send money, because your balance $0.0 .");

                    if (sender.Balance < transaction.Amount) return new CrudResponse(false, $"Sorry,You can't send money, because your balance is less than ${transaction.Amount}.");

                    if (sender.Balance > transaction.Amount)
                    {
                        sender.Balance -= transaction.Amount;
                        receiver.Balance += transaction.Amount;
                    }
                    var trans = new Transaction
                    {
                        SenderAccountNumber = transaction.SenderAccountNumber,
                        ReceiverAccountNumber = transaction.ReceiverAccountNumber,
                        TransactionDate = DateTime.UtcNow,
                        Amount = transaction.Amount
                    };
                    await AddAsync(trans);
                    await _context.SaveChangesAsync();
                    transactionOperation.Commit();
                    return new CrudResponse(true, $"The amount ${transaction.Amount} sent successfully");
                }
                catch
                {
                    transactionOperation.Rollback();
                    return new CrudResponse(false, "Something went wrong while sending money");
                }
            }
            }
        public async Task<CrudResponse> DepositAsync(DepositMoney entity)
        {
            var card = await _context.Accounts.FirstOrDefaultAsync(_ => _.AccountNumber == entity.AccountNumber);
            if (card is null) return new CrudResponse(false, $"Account is not found with number {entity.AccountNumber}");

            card.Balance += entity.Amount;
            var addNewDepositTransactions = await _context.depositMoney.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, $"Amount ${entity.Amount} is added to your account");
        }
        public async Task<List<DepositMoney>> GetDepositTransactionAsync() => await _context.depositMoney.ToListAsync();
        public async Task<CrudResponse> DeleteDepositAsync(int id)
        {
            var check = await _context.depositMoney.FirstOrDefaultAsync(_ => _.Id == id);
            if (check is null)
                return new CrudResponse(false, $"Deposit with the id [{id}] is not found");
            _context.depositMoney.Remove(check);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, $"Deposit Deleted Successfully");
        }
        public async Task<List<DepositMoney>> GetDepositByAccountNumber(string accountNumber)
        {
            try
            {
                var depositAcc = await _context.depositMoney.Where(_ => _.AccountNumber == accountNumber).ToListAsync();
                return depositAcc.Any() ? depositAcc : new List<DepositMoney>();
            }
            catch
            {
                return new List<DepositMoney>();
            }
        }
    }
}
