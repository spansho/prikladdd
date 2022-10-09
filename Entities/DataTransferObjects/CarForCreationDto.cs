using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CarForCreationDto
    {

        [Required(ErrorMessage = "Car name is a required field.")]
        public string CarName { get; set; }

        [Required(ErrorMessage = "Dollar Cost number is a required field.")]
        public int DollarCost { get; set; }

        
    

    }
}
