using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public interface INavMenuViewModel
    {
        public Command MoveToAccountView { get; set; }
        public Command MoveToGroupsView { get; set; }
        public Command MoveToGroupView { get; set; }
        public Command Logout { get; set; }
        public ContentControl NavMenuPanel { get; set; }

        public void SetHome();
    }
}
