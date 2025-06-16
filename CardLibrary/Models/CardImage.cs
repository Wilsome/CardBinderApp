using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardImage
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
       
        [Required]
        public string CardId { get; set; }

        [Required]
        [Url]
        [MaxLength(300)]
        public string ImageUrl { get; set; }
        public bool IsUserUploaded { get; set; }
        public bool IsPlaceholder { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        //Navigation Property to Card
        public CardBase Card { get; set; }

    }
}
