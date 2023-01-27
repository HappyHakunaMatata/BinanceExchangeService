using System;
namespace ExchangeService.Models.Structures
{
    public struct Filter
    {
        public string filterType { get; set; }
        public decimal minQty { get; set; }
        public decimal maxQty { get; set; }
        public decimal stepSize { get; set; }
        public decimal minNotional { get; set; }
        public decimal avgPriceMins { get; set; }
        public decimal multiplierUp { get; set; }
        public decimal multiplierDown { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public decimal tickSize { get; set; }
    }
}

