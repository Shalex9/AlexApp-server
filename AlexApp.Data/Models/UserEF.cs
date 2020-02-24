using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlexApp.Data.Models
{
    [Table("Users")]
    public class UserEF
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
