using AutoMapper;
using FintechWebAPI.Models;
using FintechWebAPI.Repositories;
using FintechWebAPI.Models.DTOs;

namespace FintechWebAPI.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(TransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDTO> CreateTransaction(TransactionDTO transactionRequest)
        {
            var transaction = _mapper.Map<Transaction>(transactionRequest);

            // Configura la fecha actual para la transacción
            transaction.Date = DateTime.UtcNow;

            // Agrega y guarda la transacción
            await _transactionRepository.AddTransaction(transaction);

            // Devuelve el DTO mapeado
            return _mapper.Map<TransactionResponseDTO>(transaction);
        }

        public async Task<TransactionResponseDTO> GetTransaction(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id);
            if (transaction == null) return null;

            return _mapper.Map<TransactionResponseDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetTransactions()
        {
            var transactions = _transactionRepository.GetTransactions();

            // Mapea la lista de transacciones al DTO
            return _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
        }

        public async Task<TransactionResponseDTO> UpdateTransaction(int id, TransactionDTO transactionRequest)
        {
            var transaction = _transactionRepository.GetTransaction(id);
            if (transaction == null) return null;

            // Actualiza las propiedades de la transacción
            transaction.TargetAccountId = transactionRequest.TargetAccountId;
            transaction.Amount = transactionRequest.Amount;
            transaction.TransactionType = transactionRequest.TransactionType;

            // Actualiza y guarda los cambios
            _transactionRepository.UpdateTransaction(transaction);

            // Devuelve el DTO actualizado
            return _mapper.Map<TransactionResponseDTO>(transaction);
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id);
            if (transaction == null) return false;

            // Elimina la transacción y guarda los cambios
            _transactionRepository.DeleteTransaction(transaction);

            return true;
        }
    }
}