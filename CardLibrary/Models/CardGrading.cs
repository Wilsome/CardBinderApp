using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardGrading
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string GradingId { get; set; }  // ✅ Match ID type to CardBase’s `GradingId`

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Range(0, 10)]
        public int Grade { get; set; }

        [Required]
        [MaxLength(50)]
        public string CertificationNumber { get; set; } = string.Empty;
        public DateOnly GradedDate { get; set; }

        // Navigation Property
        public CardBase Card { get; set; }
    }

}
