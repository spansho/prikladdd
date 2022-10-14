using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EngineDto
    {
        public Guid Id { get; set; }
        public string EngineName { get; set; }
        public int MiliageLimitkm { get; set; }
        public int EngineHorsePower { get; set; }
    }
}
