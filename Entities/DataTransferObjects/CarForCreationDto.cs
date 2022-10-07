using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CarForCreationDto
    {
       
        public string CarName { get; set; }
       

        public int DollarCost { get; set; }

        
        public Guid EngineId { get; set; }

    }
}
