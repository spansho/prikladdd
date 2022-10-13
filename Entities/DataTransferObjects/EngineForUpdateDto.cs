using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EngineForUpdateDto
    {
        [Required(ErrorMessage = "Engine Name  is a required field.")]
        public string EngineName { get; set; }

        [Required(ErrorMessage = "Miliage Limit Km   is a required field.")]
        public int MiliageLimitKm { get; set; }
        [Required(ErrorMessage = "Engine Horse Power    is a required field.")]
        public int EngineHorsePower { get; set; }
        [Required(ErrorMessage = "Cars   is a required field.")]
        public IEnumerable<CarForCreationDto> Cars { get; set; }
    }
}
