using System;
using ExchangeService.Models.Structures;

namespace ExchangeService.DataBase.Interfaces
{
    public interface IHistoryAsset
    {
        public HistoryAsset getModelAsset(string name);
        public void SaveData(HistoryAsset asset);
     }
}

