using System;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Interfaces
{
    public interface IFee
    {
        public List<Fee> GetFeeModel(string id);
    }
}

