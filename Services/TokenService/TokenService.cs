using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Services.TokenService
{
    public class TokenService : ITokenService
    {
        public int UserID { get; set; }
        public string Jwt { get; set; } = string.Empty;
    }
}
