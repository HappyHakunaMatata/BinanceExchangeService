using System.Xml.Linq;
using ExchangeService.Implementations.Abstractions;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Realisations
{
    public class ExchangeInfoImplementation : BaseImplementation<ExchangeInfo>, IExchangeInfo
    {
        private decimal MinNotional { get; set; }
        private decimal MinQty { get; set; }
        private decimal MaxQty { get; set; }
        private decimal StepSize { get; set; }

        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(new Link().exchangeInfo);
            return request.UnauthorizedRequest(id);
        }

        public Filter GetFilterModel(string name)
        {
            InitiateClass();
            try
            {
                foreach (SymbolExchangeInfo i in model.symbols)
                {
                    if (i.symbol == name)
                    {
                        MinNotional = i.filters[3].minNotional;
                        MinQty = i.filters[2].minQty;
                        MaxQty = i.filters[2].maxQty;
                        StepSize = i.filters[2].stepSize;
                        return new Filter()
                        {
                            minNotional = i.filters[3].minNotional,
                            minQty = i.filters[2].minQty,
                            maxQty = i.filters[2].maxQty,
                            stepSize = i.filters[2].stepSize
                        };
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("ExchangeInfoImplementation:" + e);
            }
            return new Filter();
        }

    }
}

