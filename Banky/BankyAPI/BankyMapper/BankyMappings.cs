using AutoMapper;
using BankyAPI.Models;
using BankyAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankyAPI.BankyMapper
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BankyMappings : Profile
    {
        public BankyMappings()
        {
            //--------This creates Mapping between Bank.cs and BankDTO.cs both ways
            CreateMap<Bank, BankDTO>().ReverseMap();
        }
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
