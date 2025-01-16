using AutoMapper;
using FintechWebAPI.Models;
using FintechWebAPI.Models.DTOs;

namespace FintechWebAPI.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // Mapeo para Account -> AccountDTO (usado en solicitudes de creación/actualización)
            CreateMap<Account, AccountDTO>();

            // Mapeo para Account -> AccountResponseDTO (incluyendo transacciones)
            CreateMap<Account, AccountResponseDTO>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.Select(t => new TransactionResponseDTO
                {
                    Id = t.Id,
                    SourceAccountId = t.SourceAccountId,
                    TargetAccountId = t.TargetAccountId,
                    Amount = t.Amount,
                    Date = t.Date,
                    TransactionType = t.TransactionType
                })));
        }
    }
}
