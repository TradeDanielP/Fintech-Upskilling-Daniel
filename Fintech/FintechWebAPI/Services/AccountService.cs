using FintechWebAPI.Models;
using FintechWebAPI.Models.DTOs;
using FintechWebAPI.Repositories;

namespace FintechWebAPI.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponseDTO> GetAccount(int id)
        {
            var account = _accountRepository.GetAccount(id);
            if (account == null) throw new KeyNotFoundException("Account not found.");

            return new AccountResponseDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Holder = account.Holder,
                Balance = account.Balance,
                AccountType = account.AccountType
            };
        }

        public async Task<AccountResponseDTO> CreateAccount(AccountDTO accountDTO)
        {
            var account = new Account
            {
                AccountNumber = accountDTO.AccountNumber,
                Holder = accountDTO.Holder,
                Balance = accountDTO.Balance,
                AccountType = accountDTO.AccountType
            };
            _accountRepository.AddAccount(account);

            return new AccountResponseDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Holder = account.Holder,
                Balance = account.Balance,
                AccountType = account.AccountType
            };
        }

        public async Task<AccountResponseDTO> UpdateAccount(int id, AccountDTO accountDTO)
        {
            var account = _accountRepository.GetAccount(id);
            if (account == null) throw new KeyNotFoundException("Account not found.");

            account.AccountNumber = accountDTO.AccountNumber;
            account.Holder = accountDTO.Holder;
            account.Balance = accountDTO.Balance;
            account.AccountType = accountDTO.AccountType;

            _accountRepository.UpdateAccount(account);

            return new AccountResponseDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Holder = account.Holder,
                Balance = account.Balance,
                AccountType = account.AccountType
            };
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var account = _accountRepository.GetAccount(id);
            if (account == null) return false;

            _accountRepository.DeleteAccount(id);
            return true;
        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAccounts()
        {
            var accounts = _accountRepository.GetAccounts();
            return accounts.Select(account => new AccountResponseDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Holder = account.Holder,
                Balance = account.Balance,
                AccountType = account.AccountType
            }).ToList();
        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAccountsByType(string accountType)
        {
            var accounts = _accountRepository.GetAccountsByType(accountType);
            return accounts.Select(account => new AccountResponseDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Holder = account.Holder,
                Balance = account.Balance,
                AccountType = account.AccountType
            }).ToList();
        }
    }
}
