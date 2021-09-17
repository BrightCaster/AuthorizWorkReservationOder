using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WorkersOrder.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "The Surname is not specified")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "The Name is not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Login is not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="The Role is not specified")]
        public int Role { get; set; }
    }
}
