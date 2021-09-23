using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkersOrder.Models.ViewModels
{
    public class WorkPlaceModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int DevicesID { get; set; }
        [Required]
        public string Discription { get; set; }
    }
}
