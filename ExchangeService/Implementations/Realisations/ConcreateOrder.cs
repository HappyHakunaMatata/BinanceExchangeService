using System;
using System.Collections.Generic;
using ExchangeService.Implementations.Abstractions;
using ExchangeService.Models.Structures;
using ExchangeService.DataBase;
using Microsoft.EntityFrameworkCore;
using ExchangeService.ViewModels;
using System.Xml.Linq;


namespace ExchangeService.Implementations.Realisations
{
    public class ConcreateOrder : BaseImplementation<Response>
    {
        private string _link { get; set; } = "";
        private string _URI = new Link().order;

        public override HttpResponseMessage Authtorisation(string id = "")
        {
            BinanceConcreateRequest request = new BinanceConcreateRequest(_URI);
            return request.PostRequest("", _link);
        }

        public Response CreateRequest()
        {
            if (model != null)
            {
                return model;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public ConcreateOrder(DashBoardViewModel model)
        {
            _link = $"symbol={model.TickerName}&side={model.Side}&type=MARKET" +
                $"{model.quantity_parametr}={model.GetAvalibleAmountForOrder()}";
        }
    }
}

