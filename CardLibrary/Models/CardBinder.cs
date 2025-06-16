using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardBinder
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal EstimatedValue { get; set; }

        [Range(0, int.MaxValue)]
        public int CardCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Foreign Key - binder belongs to a collection
        [Required]
        public string CollectionId { get; set; }
        public Collection Collection { get; set; }

        public List<CardBase> Cards { get; set; } = [];
    }
}
