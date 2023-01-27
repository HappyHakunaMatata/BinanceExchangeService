using System;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Interfaces
{
    public interface IKline
    {
        public Kline GetKlineModel(string id = "");
    }
}

