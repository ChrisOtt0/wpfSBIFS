using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Services.SessionService
{
    public interface ISessionService
    {
        public string CurrentUser { get; set; }
        public int CurrentGroup { get; set; }
        public int CurrentActivity { get; set; }
    }
}
