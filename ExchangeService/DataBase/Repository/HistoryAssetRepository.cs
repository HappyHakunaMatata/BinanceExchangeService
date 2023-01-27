using System;
using ExchangeService.DataBase.Interfaces;
using ExchangeService.Models.Structures;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace ExchangeService.DataBase.Repository
{
    public class HistoryAssetRepository : IHistoryAsset
    {
        private readonly AppDBContext appDBContent;

        public HistoryAssetRepository(AppDBContext appDBContent)
        {
            this.appDBContent = appDBContent;
        }


        public HistoryAsset getModelAsset(string name)
        {
            try
            {
                var db_model = appDBContent.Asset.OrderBy(d => d.date).LastOrDefault(n => n.name == name);
                if (db_model == null)
                {
                    return new HistoryAsset();
                }
                else
                {
                    return db_model;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("HistoryAssetRepository: " + e);
                return new HistoryAsset();
            }
        }

        public void SaveData(HistoryAsset asset)
        {
            try
            {
                appDBContent.Asset.Add(asset);
                appDBContent.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("HistoryAssetRepository: " + e);
            }
        }
    }
}

