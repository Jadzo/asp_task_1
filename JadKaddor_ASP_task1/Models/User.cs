using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JadKaddor_ASP_task1.Models
{
    public class Userauth
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string Email { get; set; }

        public string Tkn { get; set; }
        [Required]
        public string phoneNumber { get; set; }
    }
}