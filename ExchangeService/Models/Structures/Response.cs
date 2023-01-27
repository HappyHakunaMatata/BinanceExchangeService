using System;
namespace ExchangeService.Models.Structures
{
    public class Response
    {
        public string? symbol { get; set; }
        public decimal transactTime { get; set; }
        public string filter { get; set; } = "";
        public decimal profit { get; set; }
        public decimal range { get; set; }
    }
}

