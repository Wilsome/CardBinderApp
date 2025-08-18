using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class CardBaseDto
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public CardCondition Condition { get; set; }
        public string BinderId { get; set; }

        //navigation 
        public string? GradingId { get; set; }
        //public CardImageDto? Image { get; set; }

    }
}
