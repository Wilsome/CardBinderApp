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
        [ForeignKey("Card")]
        public string CardId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsUserUploaded { get; set; }
        public bool IsPlaceholder { get; set; }
        public DateTime UploadedAt { get; set; }

        //Navigation Property to Card
        public CardBase Card { get; set; }

    }
}
