using ExchangeService.Implementations.Abstractions;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;


namespace ExchangeService.Implementations.Realisations
{
    public class OrderImplementation : BaseImplementation<Order>, IOrder
    {
        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(new Link().order);
            return request.UnauthorizedRequest("", $"symbol={id}");
        }

        public Order GetOrderModel(string ticker)
        {
            InitiateClass(ticker);
            try
            {
                return model;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("OrderImplementation: " + e);
            }
            return new Order();
        }
    }
}

