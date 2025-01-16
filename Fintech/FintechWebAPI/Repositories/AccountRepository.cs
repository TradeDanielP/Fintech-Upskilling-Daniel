using FintechWebAPI.Models;
using FintechWebAPI.Data;

namespace FintechWebAPI.Repositories
{
    public class AccountRepository
    {
        private readonly FintechDbContext _dbContext;

        public AccountRepository(FintechDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccount(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public void AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(int id)
        {
            var account = GetAccount(id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAccountsByType(string accountType)
        {
            return _dbContext.Accounts.Where(a => a.AccountType == accountType).ToList();
        }
    }
}
