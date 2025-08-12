using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class CreateCollectionDto
    {
        public string Name { get; set; }
        public CollectionTheme Theme { get; set; }
        public decimal EstimatedValue { get; set; }
        public string UserId { get; set; }

    }
}
