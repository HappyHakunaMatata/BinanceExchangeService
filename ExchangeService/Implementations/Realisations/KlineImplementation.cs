using ExchangeService.Implementations.Abstractions;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;
using System.Text.Json;

namespace ExchangeService.Implementations.Realisations
{
    public class KlineImplementation : BaseImplementation<List<List<object>>>, IKline
    {
        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(new Link().Klines);
            return request.UnauthorizedRequest("", $"symbol={id}&interval=5m");
        }

        public Kline GetKlineModel(string ticker)
        {
            InitiateClass(ticker);
            if (model != null)
            {
                return new Kline()
                {
                    OpenPrice = Convert.ToDecimal(model[model.Count - 1][1].ToString()?.Replace(".", ",")),
                    ClosePrice = Convert.ToDecimal(model[model.Count - 1][4].ToString()?.Replace(".", ",")),
                };
            }
            else 
            {
                throw new NullReferenceException();
            }
        }

    }
}

