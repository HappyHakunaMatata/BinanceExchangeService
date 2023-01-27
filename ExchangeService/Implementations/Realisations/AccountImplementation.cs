using ExchangeService.Implementations.Abstractions;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;


namespace ExchangeService.Implementations.Realisations
{
    public class AccountImplementation : BaseImplementation<Account>, IAccount
    {

        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(new Link().Wallet);
            return request.AuthorizedRequest(id);
        }

        public Balance GetBalanceModel(string name)
        {
            InitiateClass();
            try
            {
                foreach (Balance i in model.balances)
                {
                    if (i.asset == name)
                    {
                        return i;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("AccountImplementation: " + e);
            }
            return new Balance();
        }

    }
}

