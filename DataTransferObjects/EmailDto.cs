using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.DataTransferObjects
{
    public class EmailDto : IJson
    {
        public string Email { get; set; } = string.Empty;
    }
}
