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

        [Required]
        public string CardId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.UtcNow;

        //Navigation
        public CardBase Card { get; set; }
    }
}
