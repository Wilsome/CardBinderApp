using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.DTO
{
    public class CollectionResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Theme  { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BinderCount { get; set; }
    }
}
