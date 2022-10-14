using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CarForUpdateDto
    {

        [Required(ErrorMessage = "Car name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string CarName { get; set; }

        [Required(ErrorMessage = "Dolar Cost  is a required field.")]
        public int DollarCost { get; set; }

    }
}
