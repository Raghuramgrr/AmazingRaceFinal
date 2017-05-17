using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingRace.Model
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public String UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public String Password { get; set; }

    }
}
