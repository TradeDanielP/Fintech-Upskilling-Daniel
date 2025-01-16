using AutoMapper;
using FintechWebAPI.Models;
using FintechWebAPI.Models.DTOs;

namespace FintechWebAPI.AutoMapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            // Mapeo entre Transaction y TransactionDTO
            CreateMap<Transaction, TransactionDTO>().ReverseMap();

            // Mapeo entre Transaction y TransactionResponseDTO
            CreateMap<Transaction, TransactionResponseDTO>().ReverseMap();
        }
    }
}
