using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.SessionService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class GroupsViewModel : Bindable, IGroupsViewModel
    {
        private readonly IHttpService _http;
        private readonly ITokenService _token;
        private readonly INavigationService _nav;
        private readonly ISessionService _session;
        private string baseUrl = "AdminGroup/";
        private string feedbackLabel = string.Empty;
        private string email = string.Empty;
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string FeedbackLabel
        {
            get => feedbackLabel;
            set
            {
                feedbackLabel = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged();
            }
        }

        public Command Search { get; set; }
        public Command NewGroup { get; set; }
        public Command EditGroup { get; set; }

        public GroupsViewModel(IHttpService http, ITokenService token, INavigationService nav, ISessionService session)
        {
            _http = http;
            _token = token;
            _nav = nav;
            _session = session;

            Search = new Command(SearchCommand);
            NewGroup = new Command(NewGroupCommand);
            EditGroup = new Command(EditGroupCommand);


        }

        public async void OnInit()
        {
            if (_session.CurrentUser == string.Empty)
                return;

            Email = _session.CurrentUser;
            SearchCommand(null);
        }

        private async void SearchCommand(object parameter)
        {
            string url = "ReadMany";
            await SendCommand(url);
        }

        private async void NewGroupCommand(object parameter)
        {
            string url = "Create";
            await SendCommand(url);
        }

        private async void EditGroupCommand(object parameter)
        {
            Group g = (Group)parameter;
            _session.CurrentGroup = g.GroupID;
            _nav.MoveToGroupView.Execute(parameter);
        }

        private async Task SendCommand(string url)
        {
            FeedbackLabel = "Loading..";

            if (Email == string.Empty)
            {
                FeedbackLabel = "No email given.";
                return;
            }

            IJson data = new EmailDto
            {
                Email = this.Email,
            };
            HttpResponseMessage response = await _http.Post(baseUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }
            if (!((int)response.StatusCode == 200))
            {
                _session.CurrentUser = Email;
                Groups = new ObservableCollection<Group>();
                FeedbackLabel = "User has no groups.";
                return;
            }

            List<Group> received = await response.Content.ReadFromJsonAsync<List<Group>>();
            if (received == null || received.Count == 0)
            {
                FeedbackLabel = "Search successful but user has no groups.";
                return;
            }

            _session.CurrentUser = Email;
            Groups = new ObservableCollection<Group>(received);
            FeedbackLabel = string.Empty;
        }
    }
}
