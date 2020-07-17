using System;
using System.ComponentModel.DataAnnotations;

namespace Jinder.Auth.Models
{
    public class Account
    {
        [Key]
        public Guid Token { get; set; }

        public String Login { get; set; }
        public String Password { get; set; }

        public Int32 UserId { get; set; }
    }
}