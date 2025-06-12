using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardPriceHistory
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Card")]
        public string CardId { get; set; }
        public decimal Value { get; set; }
        public DateTime DateRecorded { get; set; }

        //Navigation
        public CardBase Card { get; set; }
    }
}
