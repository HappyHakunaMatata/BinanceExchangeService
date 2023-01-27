using System;
namespace ExchangeService.Models.Structures
{
    public struct OpenOrder
    {
        public string symbol { get; set; }
        public decimal orderId { get; set; }
        public decimal orderListId { get; set; }
        public string clientOrderId { get; set; }
        public string price { get; set; }
        public string origQty { get; set; }
        public string executedQty { get; set; }
        public string cummulativeQuoteQty { get; set; }
        public string status { get; set; }
        public string timeInForce { get; set; }
        public string type { get; set; }
        public string side { get; set; }
        public string stopPrice { get; set; }
        public string icebergQty { get; set; }
        public decimal time { get; set; }
        public decimal updateTime { get; set; }
        public bool isWorking { get; set; }
        public string origQuoteOrderQty { get; set; }
    }
}

