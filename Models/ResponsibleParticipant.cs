using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Models
{
    public class ResponsibleParticipant
    {
        public User Participant { get; set; }
        public bool IsResponsible { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
