using FintechWebAPI.Models;
using FintechWebAPI.Data;

namespace FintechWebAPI.Repositories
{
    public class TransactionRepository
    {
        private readonly FintechDbContext _context;

        public TransactionRepository(FintechDbContext context)
        {
            _context = context;
        }

        public Transaction GetTransaction(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }
    }
}