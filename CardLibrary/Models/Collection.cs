using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class Collection
    {
        public string  Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CollectionTheme Theme { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now ;

        //Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }

        //Navigation property
        public List<CardBinder> Binders { get; set; } = [];
    }
}
