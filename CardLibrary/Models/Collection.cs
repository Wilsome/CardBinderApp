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
    public class Collection
    {
        [Key]
        public string  Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CollectionTheme Theme { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow ;

        //Foreign Key
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        //Navigation property
        public List<CardBinder> Binders { get; set; } = [];
    }
}
