using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class CreateCardDto
    {
        public string Name { get; set; }
        public CardCondition Condition { get; set; }
        public decimal? Value { get; set; }
        public string BinderId { get; set; }
        
        public UpdateCardGradingDto? Grading { get; set; }
        public UpdateCardImageDto? Image { get; set; }
    }
}
