using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unity;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public class NavMenuViewModel : Bindable, INavMenuViewModel
    {
        private readonly ITokenService _token;
        private readonly INavigationService _nav;

        public Command MoveToAccountView { get; set; }
        public Command MoveToGroupsView { get; set; }
        public Command MoveToGroupView { get; set; }
        public Command Logout { get; set; }
        public ContentControl NavMenuPanel { get; set; }

        public NavMenuViewModel(ITokenService token, INavigationService nav)
        {
            _token = token;
            _nav = nav;
            MoveToAccountView = new Command(MoveToAccountViewCommand);
            MoveToGroupsView = new Command(MoveToGroupsViewCommand);
            MoveToGroupView = new Command(MoveToGroupViewCommand);
            Logout = new Command(LogoutCommand);
            _nav.MoveToAccountView = this.MoveToAccountView;
            _nav.MoveToGroupsView = this.MoveToGroupsView;
            _nav.MoveToGroupView = this.MoveToGroupView;
        }

        public void SetHome()
        {
            NavMenuPanel.Content = App.container.Resolve<AccountView>();
        }

        private void MoveToAccountViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<AccountView>();
        }

        private void MoveToGroupsViewCommand(object parameter)
        {
            NavMenuPanel.Content = App.container.Resolve<GroupsView>();
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
