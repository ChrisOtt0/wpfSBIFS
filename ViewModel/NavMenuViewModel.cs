using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unity;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public class NavMenuViewModel : Bindable, INavMenuViewModel
    {
        public Command MoveToAccountView { get; set; }
        public Command MoveToGroupView { get; set; }
        public ContentControl NavMenuPanel { get; set; }

        public NavMenuViewModel()
        {
            MoveToAccountView = new Command(MoveToAccountViewCommand);
            MoveToGroupView = new Command(MoveToGroupViewCommand);
        }

        public void SetHome(UserControl view)
        {
            NavMenuPanel.Content = view;
        }

        private void MoveToAccountViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<AccountView>();
        }

        private void MoveToGroupViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<GroupView>();
        }
    }
}
