using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EngineForCreationDto
    {
        public string EngineName { get; set; }
        public int MiliageLimitKm { get; set; }
        public int EngineHorsePower { get; set; }
        public IEnumerable<CarForCreationDto> Cars { get; set; }
    }
}
