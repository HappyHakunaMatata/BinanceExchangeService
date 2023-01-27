using System;
using ExchangeService.Models.Structures;
using System.Collections.Generic;

namespace ExchangeService.Models.Structures
{
    public struct Account
        {
        public decimal makerCommission { get; set; }
        public decimal takerCommission { get; set; }
        public decimal buyerCommission { get; set; }
        public decimal sellerCommission { get; set; }
        public bool canTrade { get; set; }
        public bool canWithdraw { get; set; }
        public bool canDeposit { get; set; }
        public decimal updateTime { get; set; }
        public string accountType { get; set; }
        public List<Balance> balances { get; set; }
    }
}

