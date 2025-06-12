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
        [ForeignKey("Card")]
        public string CardId { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal PercentageChange { get; set; }
        public List<MarketSource> MarketSources { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<CardPriceHistory> PriceHistory { get; set; }

        //navigation 
        public CardBase Card { get; set; }
    }
}
