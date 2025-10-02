using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class ResponseCardDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public decimal Value { get; set; }
        public string BinderId { get; set; }
        public string? GradingId { get; set; }
        public string? CreatedAt { get; set; }
        public UpdateCardImageDto? Image { get; set; }
        public UpdateCardGradingDto? Grading { get; set; }


    }
}
