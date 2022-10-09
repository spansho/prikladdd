using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EngineForCreationDto
    {
        [Required(ErrorMessage = "Engine name is a required field.")]
        public string EngineName { get; set; }
        [Required(ErrorMessage = "MiliageLimitKm number is a required field.")]
        public int MiliageLimitKm { get; set; }
        [Required(ErrorMessage = "EngineHorsePower number is a required field.")]
        public int EngineHorsePower { get; set; }
        [Required(ErrorMessage = "Cars name is a required field.")]
        public IEnumerable<CarForCreationDto> Cars { get; set; }
    }
}
