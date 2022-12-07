using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.NavigationService;
using wpfSBIFS.Services.SessionService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class ActivityViewModel : Bindable, IActivityViewModel
    {
        private readonly IHttpService _http;
        private readonly ISessionService _session;
        private readonly INavigationService _nav;

        private string baseUrl = "AdminActivity/";
        private string activityUrl = "Activity/";
        private List<User> participants = new List<User>();

        private ObservableCollection<ResponsibleParticipant> responsibles = new ObservableCollection<ResponsibleParticipant>();
        private Activity Activity { get; set; } = new Activity { Description = "Loading...", Amount = 0.0 };
        private string feedbackLabel = string.Empty;

        public string FeedbackLabel 
        { 
            get => feedbackLabel; 
            set
            {
                feedbackLabel = value;
                OnPropertyChanged();
            }
        }

        public string ActivityDescription
        {
            get => Activity.Description;
            set
            {
                Activity.Description = value;
                OnPropertyChanged();
            }
        }

        public double Amount 
        {
            get => Convert.ToDouble(Activity.Amount);
            set
            {
                Activity.Amount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ResponsibleParticipant> ResponsibleParticipants 
        {
            get => responsibles;
            set
            {
                responsibles = value;
                OnPropertyChanged();
            }
        }

        public Command Save { get; set; }
        public Command Cancel { get; set; }
        public Command Delete { get; set; }

        public ActivityViewModel(IHttpService http, ISessionService session, INavigationService nav)
        {
            _http = http;
            _session = session;
            _nav = nav;

            Save = new Command(SaveCommand);
            Cancel = new Command(CancelCommand);
            Delete = new Command(DeleteCommand);
        }

        public async void OnInit()
        {
            // Load Activity
            string url = "ReadOne";
            IJson data = new ActivityDto { ActivityID = _session.CurrentActivity };

            HttpResponseMessage response = await _http.Post(activityUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                _session.CurrentActivity = 0;
                return;
            }

            this.Activity = await response.Content.ReadFromJsonAsync<Activity>();
            OnPropertyChanged("ActivityDescription");
            OnPropertyChanged("Amount");

            // Load Participants in Group
            url = "Group/ReadParticipants";
            data = new GroupDto { GroupID = _session.CurrentGroup };

            response = await _http.Post(url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                _session.CurrentGroup = 0;
                return;
            }

            participants = await response.Content.ReadFromJsonAsync<List<User>>();

            // Create ResponsibleParticipants
            if (this.Activity.Participants.Count == 0)
            {
                foreach (User p in participants)
                {
                    responsibles.Add(new ResponsibleParticipant
                    {
                        Participant = p,
                        Name = p.Name,
                        IsResponsible = true
                    });
                }
            }
            else
            {
                foreach (User p in participants)
                {
                    ResponsibleParticipant responsible = new ResponsibleParticipant
                    {
                        Participant = p,
                        Name = p.Name,
                    };

                    if (this.Activity.Participants.Where(u => u.UserID == p.UserID).FirstOrDefault() != null)
                        responsible.IsResponsible = true;
                    else
                        responsible.IsResponsible = false;

                    responsibles.Add(responsible);
                }
            }
            OnPropertyChanged("ResponsibleParticipants");

        }

        private async void SaveCommand(object parameter)
        {
            FeedbackLabel = "Saving...";
            string url = "UpdateActivity";

            // Add Participants to Activity
            this.Activity.Participants = new List<User>();
            foreach (ResponsibleParticipant p in responsibles)
            {
                if (p.IsResponsible)
                    this.Activity.Participants.Add(participants.Where(u => u.UserID == p.Participant.UserID).First());
                else
                    this.Activity.Participants.Remove(participants.Where(u => u.UserID == p.Participant.UserID).First());
            }

            // Send to API
            IJson data = Activity;
            HttpResponseMessage response = await _http.Put(activityUrl + url, data);
            
            _nav.MoveToGroupView.Execute(parameter);
        }

        private async void CancelCommand(object parameter)
        {
            _session.CurrentActivity = 0;
            _nav.MoveToGroupView.Execute(parameter);
        }

        private async void DeleteCommand(object parameter)
        {
            string url = "Delete";

            IJson data = new ActivityDto { ActivityID = this.Activity.ActivityID };
            HttpResponseMessage response = await _http.Delete(baseUrl + url, data);
            if (!response.IsSuccessStatusCode)
            {
                FeedbackLabel = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            _session.CurrentActivity = 0;
            _nav.MoveToGroupView.Execute(parameter);
        }
    }
}
