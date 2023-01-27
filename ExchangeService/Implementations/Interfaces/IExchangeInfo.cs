using System;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Interfaces
{
    public interface IExchangeInfo
    {
        public Filter GetFilterModel(string name);
    }
}

