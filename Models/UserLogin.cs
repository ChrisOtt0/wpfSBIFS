using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;

namespace wpfSBIFS.Models
{
    public class UserLogin : IJson
    {
        public int UserLoginID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
