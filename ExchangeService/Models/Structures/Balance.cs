using System;


namespace ExchangeService.Models.Structures
{
    public struct Balance
    {
        public string asset { get; set; }
        public decimal free { get; set; }
        public decimal locked { get; set; }
    }
}

