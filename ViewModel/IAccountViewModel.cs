using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpfSBIFS.ViewModel
{
    public interface IAccountViewModel
    {
        public PasswordBox OldPasswordBox { get; set; }
        public PasswordBox NewPasswordBox { get; set; }
        public PasswordBox NewPasswordAgainBox { get; set; }
    }
}
