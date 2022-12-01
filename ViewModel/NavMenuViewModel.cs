using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unity;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public class NavMenuViewModel : Bindable, INavMenuViewModel
    {
        private readonly ITokenService _token;

        public Command MoveToAccountView { get; set; }
        public Command MoveToGroupView { get; set; }
        public Command Logout { get; set; }
        public ContentControl NavMenuPanel { get; set; }

        public NavMenuViewModel(ITokenService token)
        {
            MoveToAccountView = new Command(MoveToAccountViewCommand);
            MoveToGroupView = new Command(MoveToGroupViewCommand);
            Logout = new Command(LogoutCommand);
            _token = token;
        }

        public void SetHome()
        {
            NavMenuPanel.Content = App.container.Resolve<AccountView>();
        }

        private void MoveToAccountViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<AccountView>();
        }

        private void MoveToGroupViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<GroupView>();
        }

        private void LogoutCommand(object parameter)
        {
            _token.UserID = 0;
            _token.Jwt = string.Empty;
            ((App)App.Current).ChangeUserControl(App.container.Resolve<LoginView>());
        }
    }
}
