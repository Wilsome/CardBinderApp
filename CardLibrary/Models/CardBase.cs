using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public abstract class CardBase
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public CardCondition Condition { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string BinderId { get; set; }
        public CardBinder Binder { get; set; }

        // Navigation Property for images
        public CardImage? Image { get; set; }

        // Navigation Property for graded cards
        public string? GradingId { get; set; }  // ✅ Ensure Nullable
        public CardGrading? Grading { get; set; }  // ✅ Ensure Nullable
    }

}
