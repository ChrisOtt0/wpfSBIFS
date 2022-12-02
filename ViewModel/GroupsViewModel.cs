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
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class GroupsViewModel : Bindable, IGroupsViewModel
    {
        private readonly IHttpService _http;
        private readonly ITokenService _token;
        private readonly INavigationService _nav;
        private string baseUrl = "AdminGroup/";
        private string feedbackLabel = string.Empty;
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();

        public string Email { get; set; } = string.Empty;
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

        public GroupsViewModel(IHttpService http, ITokenService token, INavigationService nav)
        {
            _http = http;
            _token = token;
            _nav = nav;

            Search = new Command(SearchCommand);
            NewGroup = new Command(NewGroupCommand);
            EditGroup = new Command(EditGroupCommand);
        }

        public async void SearchCommand(object parameter)
        {
            string url = "ReadMany";
            await SendCommand(url);
        }

        public async void NewGroupCommand(object parameter)
        {
            string url = "Create";
            await SendCommand(url);
        }

        public async void EditGroupCommand(object parameter)
        {
            Group g = (Group)parameter;
            _token.CurrentGroup = g.GroupID;
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
                FeedbackLabel = "Error: "
                    + response.StatusCode.ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            List<Group> received = await response.Content.ReadFromJsonAsync<List<Group>>();
            if (received == null)
            {
                FeedbackLabel = "Search successful but user has no groups.";
                return;
            }

            Groups = new ObservableCollection<Group>(received);
            FeedbackLabel = string.Empty;
        }
    }
}
