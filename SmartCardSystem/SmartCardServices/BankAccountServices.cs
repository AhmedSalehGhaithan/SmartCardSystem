using Microsoft.EntityFrameworkCore;
using SmartCardSystem.Data;
using SmartCardSystem.SmartCardDTO;
using SmartCardSystem.SmartCardRepository;
using SmartCardSystem.SmartCardResponses;
namespace SmartCardSystem.SmartCardServices
{
    public class BankAccountServices(HttpClient _httpClient,AppDbContext _context) : IAccountRepository
    {
        public async Task<CrudResponse> AddAsync(AccountDTO account)
        {
            if (account == null)
                return new CrudResponse(false, "The account entity cannot be null.");

            // Generate a random 10-digit account number
            Random random = new Random();
            string generatedAccountNumber = random.Next(100000000, 999999999).ToString();

            // Check if the generated account number already exists
            var check = await _context.Accounts.FirstOrDefaultAsync(_u_ => _u_.AccountNumber == generatedAccountNumber);
            if (check != null)
            {
                // If it exists, generate a new one recursively
                return await AddAsync(account);
            }

            // If it doesn't exist, assign the generated number to the account
            account.AccountNumber = generatedAccountNumber;

            var addNewAccount = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, "Account created successfully");
        }

        public async Task<CrudResponse> DeleteAsync(int id)
        {
            var check = await _context.Accounts.FirstOrDefaultAsync(_ => _.Id == id);
            if (check is null)
                return new CrudResponse(false, $"Account with the id [{id}] is not found");
            _context.Accounts.Remove(check);
            await _context.SaveChangesAsync();
            return new CrudResponse(true, $"Account Deleted successfully");
        }

        public async Task<List<AccountDTO>> GetAllAsync() => await _context.Accounts.ToListAsync();
        
        public async Task<AccountDTO> GetByIdAsync(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(_u => _u.Id == id);
            if (account is null) return null!;
            return account!;
        }

        public async Task<AccountDTO> GetByAccountNumberAsync(string accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(_u => _u.AccountNumber == accountNumber);
            if (account is null) return null!;
            return account!;
        }

        public async Task<CrudResponse> UpdateAsync(AccountDTO entity)
        {
            var httpResponse = await _httpClient.PutAsJsonAsync($"api/BankAccount/Update-Account", entity);
            if (httpResponse.IsSuccessStatusCode)
                return new CrudResponse(true, "Account Updated successfully.");

            else
            {
                var check = await _context.Accounts.Where(_u_ => _u_.Id == entity.Id).FirstOrDefaultAsync();
                if (check is null) return new CrudResponse(false, $"Account with id [{entity.Id}] is not found");
                _context.Accounts.Update(entity);
                await _context.SaveChangesAsync();
                return new CrudResponse(true, $"Account updated successfully");
            }
        }
    }
}
