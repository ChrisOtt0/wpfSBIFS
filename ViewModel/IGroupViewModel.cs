using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public interface IGroupViewModel
    {
        public int GroupID { get; }
        public Command Cancel { get; set; }
    }
}
