using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WorkersOrder.Models.ViewModels
{
    public class ReservationModel
    {
        [Required]
        public int ReservationID { get; set; }
        [Required]
        public int IDWorker { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
