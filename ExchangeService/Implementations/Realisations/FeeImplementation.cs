using System;
using ExchangeService.Implementations.Abstractions;
using ExchangeService.Implementations.Interfaces;
using ExchangeService.Models.Structures;

namespace ExchangeService.Implementations.Realisations
{
    public class FeeImplementation : BaseImplementation<List<Fee>>, IFee
    {
        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(new Link().fee);
            return request.AuthorizedRequest("", $"symbol={id}");
        }

        public List<Fee> GetFeeModel(string id)
        {
            InitiateClass(id);
            if(model != null)
            {
                return model;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}

