using System;
using ExchangeService.Implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExchangeService.ViewModels;
using ExchangeService.Implementations.Realisations;
using ExchangeService.Models.Structures;
using ExchangeService.DataBase.Interfaces;
using System.Collections.Generic;
using ExchangeService.DataBase.Initiate;
using Microsoft.EntityFrameworkCore;
using ExchangeService.DataBase;
using Microsoft.EntityFrameworkCore.Metadata;
using ExchangeService.Data;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ExchangeService.Controllers
{
    public class DashBoardController : Controller
    {
        readonly IAccount _IWallet;
        readonly IKline _IKline;
        readonly IOrder _Order;
        readonly IExchangeInfo _ExchangeInfo;
        readonly IHistoryAsset _HistoryAsset;
        readonly IFee _IFee;

        public DashBoardController(IAccount wallet, IKline kline,
            IOrder order, IExchangeInfo exchangeInfo,
            IHistoryAsset HistoryAsset, IFee fee)
        {
            _IKline = kline;
            _IWallet = wallet;
            _Order = order;
            _ExchangeInfo = exchangeInfo;
            _HistoryAsset = HistoryAsset;
            _IFee = fee;
        }

        public IActionResult Index()
        {
            List<string> tickers = GetTickers();
            return View(tickers);
        }

        private List<string> GetTickers()
        {
            AppDataContext context = new AppDataContext();
            return context.GetTickerList(context.ReadData());
        }

        public JsonResult GetJsonTickers()
        {
            return new JsonResult(GetTickers());
        }

        [HttpGet]
        public JsonResult GetJsonModel(string id)
        {

            return new JsonResult(GetModel(id));
        }

        private DashBoardViewModel GetModel(string id)
        {
            DashBoardViewModel ViewModel = new DashBoardViewModel()
            {
                TickerName = id.Replace("/", ""),
                BaseName = id.Split("/")[1],
                QuoteName = id.Split("/")[0]
            };
            ViewModel.order = _Order.GetOrderModel(ViewModel.TickerName);
            ViewModel.filter = _ExchangeInfo.GetFilterModel(ViewModel.TickerName);
            ViewModel.Balance = _IWallet.GetBalanceModel(id);
            ViewModel.fee = _IFee.GetFeeModel(ViewModel.TickerName);
            ViewModel.historyAsset = _HistoryAsset.getModelAsset(ViewModel.TickerName.Replace(id, ""));
            ViewModel.BaseKline = _IKline.GetKlineModel(ViewModel.BaseName + "USDT");
            ViewModel.QuoteKline = _IKline.GetKlineModel(ViewModel.QuoteName + "USDT");
            return ViewModel;
        }

        [HttpGet]
        public JsonResult CreateRequest(string name)
        {
            DashBoardViewModel ViewModel = GetModel(name);
            ViewModel.CreateRequest();
            if (ViewModel.response != null)
            {
                _HistoryAsset.SaveData(ViewModel.historyAsset);
                new AppDataContext().SaveData(ViewModel.Balance.asset);
                return new JsonResult(ViewModel.response);
            }
            return new JsonResult(ViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//'<div>Time: ' + response.transactTime + '</div>' +
//'<div>Filter: ' + response.filter + '</div>' +
//'<div>Profit: ' + response.profit + '</div>' +
//'<div>Range: ' + response.range + '</div>' +