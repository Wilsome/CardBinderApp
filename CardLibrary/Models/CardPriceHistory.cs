using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardPriceHistory
    {
        public string Id { get; set; }
        public int CardId { get; set; }
        public decimal Value { get; set; }
        public DateTime DateRecorded { get; set; }

        //Navigation
        public CardBase Card { get; set; }
    }
}
