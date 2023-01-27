using System;
using ExchangeService.Models.Structures;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeService.Implementations.Interfaces
{
    public interface IAccount
    {
        public Balance GetBalanceModel(string name);
    }
}

