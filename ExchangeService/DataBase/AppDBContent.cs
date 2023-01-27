 using System;
using Microsoft.EntityFrameworkCore;
using ExchangeService.Models.Structures;

namespace ExchangeService.DataBase
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<HistoryAsset> Asset { get; set; } = null!;
    }
}

