using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Core.Views.Auth
{
    public class LoginView
    {
        public long? Id { get; set; }
        public string Password { get; set; }
    }
}
