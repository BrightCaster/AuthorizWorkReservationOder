using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkersOrder.Models
{
    public class WorkPlaces
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int DevicesID { get; set; }
        public string Discription { get; set; }
    }
}
