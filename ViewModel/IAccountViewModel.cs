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
        public string UserFeedback { get; set; }
        public string PasswordFeedback { get; set; }

        public PasswordBox OldPasswordBox { get; set; }
        public PasswordBox NewPasswordBox { get; set; }
        public PasswordBox NewPasswordAgainBox { get; set; }

        public void OnInitAsync();
    }
}
