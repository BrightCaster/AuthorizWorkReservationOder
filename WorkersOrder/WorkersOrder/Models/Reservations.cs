using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkersOrder.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationID { get; set; }
        public int? IDWorker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

    }
}
