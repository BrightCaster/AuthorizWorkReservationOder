using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkersOrder.Models
{
    public class Devices
    {
        [Key]
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
    }
}
