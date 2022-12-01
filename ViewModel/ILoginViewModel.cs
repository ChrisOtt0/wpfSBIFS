using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public interface ILoginViewModel
    {
        public string LoginEmail { get; set; }
        public string FeedbackLabel { get; set; }
        public PasswordBox PasswordBox { get; set; }
        public Command Login { get; set; }
    }
}
