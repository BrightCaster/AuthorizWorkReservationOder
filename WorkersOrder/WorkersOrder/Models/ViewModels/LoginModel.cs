using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkersOrder.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="The Login is not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
