using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public  class CarParameters : RequestParameters
    {
        public CarParameters()
        {
            OrderBy = "name";
        }
        public uint MinDollarCost { get; set; }
        public uint MaxDollarCost { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxDollarCost > MinDollarCost;
        public string SearchTerm { get; set; }


    }
}
