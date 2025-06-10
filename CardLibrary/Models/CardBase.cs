using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public abstract class CardBase
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CardCondition Condition { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int BinderId { get; set; }
        public CardBinder Binder { get; set; }

        // Navigation  Property for images
        public CardImage? Image { get; set; }
        // Navigation Property for graded cards 
        public CardGrading? Grading { get; set; }

        

    }
}
