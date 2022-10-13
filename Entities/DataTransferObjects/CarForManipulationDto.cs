using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public  class CarForManipulationDto
    {

        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string CarName { get; set; }
      
        [Range(1, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int DollarCost { get; set; }
    }
}
