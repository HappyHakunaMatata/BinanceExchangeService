using System;
using ExchangeService.Implementations.Realisations;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;
using System.Collections.Generic;
using System.Numerics;

namespace ExchangeService.ViewModels
{
    public class DashBoardViewModel
    {
        public string TickerName { get; set; } = "USDTUSDC";
        public string BaseName { get; set; } = "USDT";
        public string QuoteName { get; set; } = "USDC";
        public string Side { get; set; } = "SELL";
        public string quantity_parametr { get; set; } = "quantity";
        private Balance balance;
        public Balance Balance
        {
            set
            {
                balance = value;
                if (balance.asset == this.BaseName)
                {
                    Side = "SELL";
                    quantity_parametr = "quantity";
                }
                else
                {
                    Side = "BUY";
                    quantity_parametr = "quoteOrderQty";
                }
            }
            get
            {
                return balance;
            }
        }
        public Filter filter { get; set; }
        public Kline BaseKline { get; set; }
        public Kline QuoteKline { get; set; }
        public Order order { get; set; }
        public List<Fee> fee { get; set; } = null!;
        public HistoryAsset historyAsset { get; set; } = new HistoryAsset();

        private HistoryAsset SaveToDB(string name, decimal amount, decimal date)
        {
            HistoryAsset asset = new HistoryAsset()
            {
                name = name,
                amount = Convert.ToDecimal(amount),
                date = date
            };
            return asset;
        }

        public decimal GetKlineAmount(Kline kline)
        {
            try
            {
                return (kline.OpenPrice - kline.ClosePrice) / (kline.OpenPrice / 100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal BKline_value = 0;
        public decimal BKline
        {
            set
            {
                BKline_value = value;
            }
            get
            {
                return GetKlineAmount(this.BaseKline);
            }
        }

        private decimal QKline_value = 0;
        public decimal QKline {
            set
            {
                QKline_value = value;
            }
            get
            {
                return GetKlineAmount(this.QuoteKline);
            }
        }

        public decimal GetKlineRange()
        {
            try
            {
                return System.Math.Abs(GetKlineAmount(this.BaseKline) - GetKlineAmount(this.QuoteKline));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal range_value { get; set; }
        public decimal range
        {
            set
            {
                range_value = value;
            }
            get
            {
                return GetKlineRange();
            }
        }

        public decimal GetAvalibleAmountForOrder()
        {
            try
            {
                return this.balance.free - this.balance.free % this.filter.stepSize;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal AmountForOrder_value { get; set; }
        public decimal AmountForOrder
        {
            set
            {
                AmountForOrder_value = value;
            }
            get
            {
                return GetAvalibleAmountForOrder();
            }
        }

        public decimal GetEstimateAmount()
        {
            decimal estimate_amount = 0;
            decimal balance_amount = this.GetAvalibleAmountForOrder();
            List<List<decimal>> market;
            int i = 0;
            try
            {
                if (this.Side == "BUY")
                {
                    market = order.asks;
                }
                else
                {
                    market = order.bids;
                }
                while (balance_amount > 0)
                {
                    if (balance_amount - market[i][1] > 0)
                    {
                        estimate_amount = estimate_amount + market[i][0] * market[i][1];
                    }
                    else
                    {
                        estimate_amount = estimate_amount + balance_amount * market[i][0];
                    }
                    balance_amount = balance_amount - market[i][1];
                    i += 1;
                }
                return estimate_amount + estimate_amount * Convert.ToDecimal(this.fee[0].takerCommission);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal EstimateAmount_value { get; set; }
        public decimal EstimateAmount
        {
            set
            {
                EstimateAmount_value = value;
            }
            get
            {
                return GetEstimateAmount();
            }
        }

        public decimal GetTickerAmount()
        {
            decimal balance_amount = this.GetAvalibleAmountForOrder();
            decimal ticker = 0;
            List<List<decimal>> market;
            int i = 0;
            try
            {
                if (this.Side == "BUY")
                {
                    market = order.asks;
                }
                else
                {
                    market = order.bids;
                }
                while (balance_amount > 0)
                {
                    ticker = ticker + market[i][1];
                    balance_amount = balance_amount - market[i][1];
                    i += 1;
                }
                if (i != 0)
                {
                    return ticker / i;
                }
                return ticker;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal TickerAmount_value { get; set; }
        public decimal TickerAmount {
            set
            {
                TickerAmount_value = value;
            }
            get
            {
                return GetTickerAmount();
            }
        }

        public decimal Profit()
        {
            try
            {
                return GetEstimateAmount() - historyAsset.amount;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
        private decimal profit_value { get; set; }
        public decimal profit {
            set
            {
                profit_value = value;
            }
            get
            {
                return Profit();
            }
        }

        public string FilterAmount()
        {
            try
            {
                decimal amount = GetAvalibleAmountForOrder();
                if ((amount < this.filter.maxQty)
                    && (amount > this.filter.minNotional)
                    && (amount > historyAsset.amount))
                {
                    return "OK";
                }
                else
                {
                    return "ERROR";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Exception";
            }
        }

        public void CreateRequest()
        {
            try
            {
                if ((FilterAmount() == "OK") &&
                    (Profit() > 0) && (GetKlineRange() > 3))
                {
                    ConcreateOrder request = new ConcreateOrder(this);
                    //request.InitiateClass();
                    //response = request.CreateRequest();
                    response = new Response()
                    {
                        filter = FilterAmount(),
                        profit = Profit(),
                        range = GetKlineRange()
                    };
                    this.historyAsset = new HistoryAsset() 
                    {
                        name = this.Balance.asset,
                        amount = this.AmountForOrder,
                        date = this.response.transactTime
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Response? response { get; set; }
    }
}