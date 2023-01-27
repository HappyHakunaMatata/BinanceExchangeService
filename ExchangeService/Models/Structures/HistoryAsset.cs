using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeService.Models.Structures
{
    public class HistoryAsset
    {
        public int Id { get; set; }
        public string? name { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal amount { get; set; } = 0;
        public decimal date { get; set; }
    }
}

