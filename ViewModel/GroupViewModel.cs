using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.SessionService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public class GroupViewModel : Bindable, IGroupViewModel
    {
        private readonly IHttpService _http;
        private readonly ITokenService _token;
        private readonly INavigationService _nav;
        private readonly ISessionService _session;
        private string baseUrl = "AdminGroup/";
        private string groupUrl = "Group/";
        private string userUrl = "User/";
        private List<UserDto> allUsers = new List<UserDto>();
        private List<UserDto> availableUsers = new List<UserDto>();
        private List<UserDto> participantDtos = new List<UserDto>();

        private string feedbackLabel = string.Empty;
        private string searchEmail = string.Empty;
        private bool popupIsOpen = false;
        private ObservableCollection<User> participants = new ObservableCollection<User>();
        private ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
        private ObservableCollection<UserDto> searchPopup = new ObservableCollection<UserDto>();

        public Group Group { get; set; } = new Group { Name = "Loading..." };
        
        public int GroupID
        {
            get => _session.CurrentGroup;
        }
        
        public string GroupName
        {
            get => Group.Name;
            set
            {
                Group.Name = value;
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

        public string SearchEmail
        {
            get => searchEmail;
            set
            {
                searchEmail = value;
                OnPropertyChanged();
            }
        }

        public bool PopupIsOpen
        {
            get => popupIsOpen;
            set
            {
                popupIsOpen = value;
                OnPropertyChanged();
            }
        }

        public GroupView View { get; set; }

        public Command Save { get; set; }
        public Command Cancel { get; set; }
        public Command Calculate { get; set; }
        public Command AddParticipant { get; set; }
        public Command RemoveParticipant { get; set; }
        public Command AddActivity { get; set; }
        public Command EditActivity { get; set; }
        public Command SelectionChanged { get; set; }

        public ObservableCollection<User> Participants
        {
            get => participants;
            set
            {
                participants = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Activity> Activities 
        { 
            get => activities; 
            set
            {
                activities = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserDto> SearchPopup 
        { 
            get => searchPopup; 
            set
            {
                searchPopup = value;
                OnPropertyChanged();
            } 
        }

        public GroupViewModel(IHttpService http, ITokenService token, INavigationService nav, ISessionService session)
        {
            _http = http;
            _token = token;
            _nav = nav;
            _session = session;

            Cancel = new Command(CancelCommand);
            Save = new Command(SaveCommand);
            Calculate = new Command(CalculateCommand);
            AddParticipant = new Command(AddParticipantCommand);
            RemoveParticipant = new Command(RemoveParticipantCommand);
            AddActivity = new Command(AddActivityCommand);
            EditActivity = new Command(EditActivityCommand);
            SelectionChanged = new Command(SelectionChangedCommand);
        }

        public async void OnInit()
        {
            // ReadGroup Part
            string url = "ReadOne";

            IJson data = new GroupDto
            {
                GroupID = _session.CurrentGroup
            };

            HttpResponseMessage response = await _http.Post(groupUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                Group = new Group
                {
                    Name = "Error"
                };
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                _session.CurrentGroup = 0;
            }

            Group g = await response.Content.ReadFromJsonAsync<Group>();
            Group = g;
            Participants = new ObservableCollection<User>(Group.Participants);
            Activities = new ObservableCollection<Activity>(Group.Activities);
            OnPropertyChanged("GroupName");


            // ReadAllUsersPart
            url = "ReadMany";
            response = await _http.Get(userUrl + url);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
            }

            allUsers = await response.Content.ReadFromJsonAsync<List<UserDto>>();

            // Handle current participants ??
            if (Group == null)
                return;

            participantDtos = new List<UserDto>();
            foreach (User u in Group.Participants)
            {
                participantDtos.Add(allUsers.Where(user => user.UserID == u.UserID).First());
            }

            availableUsers = new List<UserDto>(allUsers);
            foreach (UserDto u in participantDtos)
                availableUsers.Remove(u);
        }

        private async void CancelCommand(object parameter)
        {
            _session.CurrentGroup = 0;
            _nav.MoveToGroupsView.Execute(parameter);
        }

        private async void SaveCommand(object parameter)
        {
            GroupName = View.groupNameLabel.Content.ToString();
            FeedbackLabel = "Saving...";
            string url = "UpdateGroup";
            IJson data = Group;

            HttpResponseMessage response = await _http.Put(groupUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            _nav.MoveToGroupsView.Execute(parameter);
        }

        private async void CalculateCommand(object parameter)
        {
            MessageBox.Show("Do math");
        }

        private async void AddParticipantCommand(object parameter)
        {
            if (SearchEmail == string.Empty)
            {
                FeedbackLabel = "No email given.";
                return; 
            }
            string url = "AddParticipant";

            GroupDto gDto = new GroupDto { GroupID = Group.GroupID };
            EmailDto eDto = new EmailDto { Email = SearchEmail };
            IJson data = new GroupUserDto
            {
                GroupRequest = gDto,
                UserRequest = eDto,
            };

            HttpResponseMessage response = await _http.Put(baseUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            Group newGroup = await response.Content.ReadFromJsonAsync<Group>();
            Group = newGroup;
            Participants = new ObservableCollection<User>(Group.Participants);
            Activities = new ObservableCollection<Activity>(Group.Activities);
            OnPropertyChanged("GroupName");

            UserDto newParticipant = allUsers.Where(u => u.Email == SearchEmail).First();
            availableUsers.Remove(newParticipant);
            participantDtos.Add(newParticipant);

            FeedbackLabel = string.Empty;
        }

        private async void RemoveParticipantCommand(object parameter)
        {
            if (parameter == null) return;
            if (!(parameter.GetType() == typeof(User))) return;

            string url = "RemoveParticipant";
            GroupDto gDto = new GroupDto { GroupID = Group.GroupID };
            EmailDto eDto = new EmailDto { Email = (participantDtos.Where(u => u.UserID == ((User)parameter).UserID).First()).Email };
            IJson data = new GroupUserDto
            {
                GroupRequest = gDto,
                UserRequest = eDto,
            };

            HttpResponseMessage response = await _http.Put(baseUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            Group newGroup = await response.Content.ReadFromJsonAsync<Group>();
            Group = newGroup;
            Participants = new ObservableCollection<User>(Group.Participants);
            Activities = new ObservableCollection<Activity>(Group.Activities);
            OnPropertyChanged("GroupName");

            UserDto oldParticipant = allUsers.Where(u => u.UserID == ((User)parameter).UserID).First();
            availableUsers.Add(oldParticipant);
            participantDtos.Remove(oldParticipant);

            FeedbackLabel = string.Empty;
        }

        private async void AddActivityCommand(object parameter)
        {
            MessageBox.Show("New Activity");
        }

        private async void EditActivityCommand(object parameter)
        {
            MessageBox.Show(parameter.GetType().ToString());
        }

        private async void SelectionChangedCommand(object parameter)
        {
            if (parameter == null) return;
            if (!(parameter.GetType() == typeof(UserDto))) return;
         
            SearchEmail = ((UserDto)parameter).Email;
            ClosePopup();
        }

        public async void SearchEmailChanged()
        {
            if (SearchEmail.Length > 2)
            {
                SearchPopup = new ObservableCollection<UserDto>(allUsers.Where(u => u.Email.StartsWith(SearchEmail)));
                OpenPopup();

            } else
            {
                ClosePopup();
            }
        }

        private void OpenPopup()
        {
            if (!(SearchPopup.Count > 0))
            {
                PopupIsOpen = false;
                return;
            }

            PopupIsOpen = true;
        }

        private void ClosePopup()
        {
            PopupIsOpen = false;
        }
    }
}
