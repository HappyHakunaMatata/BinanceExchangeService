using System;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Interfaces
{
    public interface IOrder
    {
        public Order GetOrderModel(string id = ""); 
    }
}

