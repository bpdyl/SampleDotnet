using System;
using System.Collections.Generic;
using System.Component.DataAnnotation;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Cars
    {
        [Key]
        public int carId {get; set; }
        [Required]
        public string CarName {get; set; }
        public string Description {get; set; }
        public int Price {get; set; }
    }
    
}