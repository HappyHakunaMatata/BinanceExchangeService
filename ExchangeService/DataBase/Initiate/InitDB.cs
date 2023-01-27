using System;
using ExchangeService.DataBase.Interfaces;
using ExchangeService.Models.Structures;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ExchangeService.DataBase.Repository;

namespace ExchangeService.DataBase.Initiate
{
    public class InitDB
    {
        private readonly AppDBContext _appDBContent;

        public InitDB(AppDBContext appDBContext)
        {
            _appDBContent = appDBContext;
        }

        public void Save()
        {
            HistoryAsset USDC = new HistoryAsset();
            USDC.name = "USDC";
            USDC.amount = Convert.ToDecimal(29.42088836);
            USDC.date = 1659877380;

            HistoryAsset USDT = new HistoryAsset();
            USDT.name = "USDT";
            USDT.amount = Convert.ToDecimal(29.42);
            USDT.date = 1661996400;

            HistoryAsset BTC = new HistoryAsset();
            BTC.name = "BTC";
            BTC.amount = Convert.ToDecimal(0.00125356);
            BTC.date = 1661971800;

            HistoryAsset ETH = new HistoryAsset();
            ETH.name = "ETH";
            ETH.amount = Convert.ToDecimal(0.0165571);
            ETH.date = 1664197620;

            HistoryAsset STORJ = new HistoryAsset();
            STORJ.name = "STORJ";
            STORJ.amount = Convert.ToDecimal(46.0369);
            STORJ.date = 1662485580;

            HistoryAsset BNB = new HistoryAsset();
            BNB.name = "BNB";
            BNB.amount = Convert.ToDecimal(0.07927525);
            BNB.date = 1664345493;

            _appDBContent.Asset.Add(USDT);
            _appDBContent.Asset.Add(USDC);
            _appDBContent.Asset.Add(BTC);
            _appDBContent.Asset.Add(ETH);
            _appDBContent.Asset.Add(STORJ);
            _appDBContent.SaveChanges();

        }

    }
}

