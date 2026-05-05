using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Auth
{
    public class SigninView
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "ادخل الرقم السري")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ادخل تأكيد الرقم السري")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "ادخل اللون المفضل")]
        public string Color { get; set; }
    }
}
