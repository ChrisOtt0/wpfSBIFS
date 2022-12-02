using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class GroupViewModel : IGroupViewModel
    {
        private readonly IHttpService _http;
        private readonly ITokenService _token;
        private readonly INavigationService _nav;

        public int GroupID
        {
            get => _token.CurrentGroup;
        }

        public Command Cancel { get; set; }

        public GroupViewModel(IHttpService http, ITokenService token, INavigationService nav)
        {
            _http = http;
            _token = token;
            _nav = nav;
            Cancel = new Command(CancelCommand);
        }

        private async void CancelCommand(object parameter)
        {
            _token.CurrentGroup = 0;
            _nav.MoveToGroupsView.Execute(parameter);
        }
    }
}
