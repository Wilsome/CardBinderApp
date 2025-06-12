using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Models
{
    public class MagicTheGathering : CardBase
    {
        public string SetName { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
    }
}
