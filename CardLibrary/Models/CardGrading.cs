using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class CardGrading
    {
        public string Id { get; set; }
        public int CardId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public int Grade { get; set; }
        public string CertificationNumber { get; set; } = string.Empty;
        public DateOnly GradedDate { get; set; }


        //navigation 
        public CardBase Card { get; set; }
    }
}
