using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class UpdateCardImageDto
    {
        public string? ImageUrl { get; set; }
        public bool? IsUserUploaded { get; set; }
        public bool? IsPlaceHolder { get; set; }
        public string UploadedAt { get; set; }

    }
}
