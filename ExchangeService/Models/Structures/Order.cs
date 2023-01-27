using System;
namespace ExchangeService.Models.Structures
{
    public struct Order
    {
        public decimal lastUpdateId { get; set; }
        public List<List<decimal>> bids { get; set; }
        public List<List<decimal>> asks { get; set; }
    }
}

