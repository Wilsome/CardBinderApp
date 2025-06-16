using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardValueTracker
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string CardId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MinPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MaxPrice { get; set; }
        public decimal PercentageChange { get; set; }
        public List<MarketSource> MarketSources { get; set; } = new();
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public List<CardPriceHistory> PriceHistory { get; set; } = new();

        //navigation 
        public CardBase Card { get; set; }
    }
}
