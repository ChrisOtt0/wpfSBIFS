using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Services.SessionService
{
    public class SessionService : ISessionService
    {
        public string CurrentUser { get; set; } = string.Empty;
        public int CurrentGroup { get; set; }
    }
}
