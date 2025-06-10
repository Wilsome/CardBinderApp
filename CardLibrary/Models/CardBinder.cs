using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardBinder
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal EstimatedValue { get; set; }
        public int CardCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Foreign Key - binder belongs to a collection
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public List<CardBase> Cards { get; set; } = [];
    }
}
