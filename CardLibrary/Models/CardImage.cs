using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardImage
    {
        public string Id { get; set; }
        public int CardId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsUserUploaded { get; set; }
        public bool IsPlaceholder { get; set; }
        public DateTime UploadedAt { get; set; }

        //Navigation Property to Card
        public CardBase Card { get; set; }

    }
}
