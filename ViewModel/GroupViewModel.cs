using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class GroupViewModel : Bindable, IGroupViewModel
    {
        private readonly IHttpService _http;
        private readonly ITokenService _token;
        private readonly INavigationService _nav;

        private string baseUrl = "AdminGroup/";
        private string groupUrl = "Group/";

        public int GroupID
        {
            get => _token.CurrentGroup;
        }

        public Group Group { get; set; } = new Group { Name = "Loading..." };

        public string GroupName
        {
            get => Group.Name;
            set
            {
                Group.Name = value;
                OnPropertyChanged();
            }
        }

        public Command Cancel { get; set; }

        public GroupViewModel(IHttpService http, ITokenService token, INavigationService nav)
        {
            _http = http;
            _token = token;
            _nav = nav;
            Cancel = new Command(CancelCommand);
            OnInit();
        }

        private async void OnInit()
        {
            string url = "ReadOne";

            IJson data = new GroupDto
            {
                GroupID = _token.CurrentGroup
            };

            HttpResponseMessage response = await _http.Post(groupUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                Group = new Group
                {
                    Name = response.StatusCode.ToString() + ": " + await response.Content.ReadAsStringAsync()
                };
                _token.CurrentGroup = 0;
            }

            Group g = await response.Content.ReadFromJsonAsync<Group>();
            Group = g;
            OnPropertyChanged("GroupName");
        }

        private async void CancelCommand(object parameter)
        {
            _token.CurrentGroup = 0;
            _nav.MoveToGroupsView.Execute(parameter);
        }
    }
}
