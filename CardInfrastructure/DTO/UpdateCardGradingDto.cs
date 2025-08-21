using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class UpdateCardGradingDto
    {
        public string? CompanyName { get; set; }
        public int? Grade { get; set; }
        public string? CertificationNumber { get; set; }
        public DateOnly? GradedDate { get; set; }
    }
}
