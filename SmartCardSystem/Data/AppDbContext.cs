using Microsoft.EntityFrameworkCore;
using SmartCardSystem.Models;
using SmartCardSystem.Repository;
using SmartCardSystem.SmartCardDTO;
namespace SmartCardSystem.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<AccountDTO> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DepositMoney> depositMoney { get; set; }
    }
}
