using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity.Injection;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public class AccountViewModel : Bindable, IAccountViewModel
    {
        private string name = string.Empty;
        private string email = string.Empty;
        private string userFeedback = string.Empty;
        private string passwordFeedback = string.Empty;
        private string baseUrl = "User/";
        private readonly IHttpService _http;
        private UserDto user;

        
        public Command SaveChanges { get; set; }
        public Command UpdatePassword { get; set; }
        public PasswordBox OldPasswordBox { get; set; }
        public PasswordBox NewPasswordBox { get; set; }
        public PasswordBox NewPasswordAgainBox { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string UserFeedback 
        { 
            get => userFeedback; 
            set
            {
                userFeedback = value;
                OnPropertyChanged();
            }
        }
        public string PasswordFeedback 
        { 
            get => passwordFeedback; 
            set
            {
                passwordFeedback = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel(IHttpService http)
        {
            SaveChanges = new Command(SaveChangesCommand);
            UpdatePassword = new Command(UpdatePasswordCommand);
            _http = http;
        }

        public async void OnInitAsync()
        {
            string url = "ReadOne";
            HttpResponseMessage response = await _http.Get(baseUrl + url);

            if (!response.IsSuccessStatusCode)
                UserFeedback = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();

            user = await response.Content.ReadFromJsonAsync<UserDto>();
            Name = user.Name;
            Email = user.Email;
        }

        private async Task SaveChangesCommand()
        {
            UserFeedback = "Saving changes...";
            string url = "UpdateUser";

            IJson data = new UserDto() 
            {
                Name = this.Name, 
                Email = this.Email
            };
            HttpResponseMessage response = await _http.Put(baseUrl + url, data );
            
            if (!response.IsSuccessStatusCode)
            {
                UserFeedback = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            UserFeedback = "Changes saved successfully.";
        }

        private async Task UpdatePasswordCommand() 
        {
            PasswordFeedback = "Updating password...";
            string url = "UpdatePassword";

            if (OldPasswordBox.Password == string.Empty || NewPasswordBox.Password == string.Empty || NewPasswordAgainBox.Password == string.Empty)
            {
                PasswordFeedback = "Please fill in all relevant fields.";
                return;
            }

            if (NewPasswordBox.Password != NewPasswordAgainBox.Password)
            {
                PasswordFeedback = "New passwords do not match";
                return;
            }
            
            IJson data = new PasswordDto() 
            {
                OldPassword = OldPasswordBox.Password, 
                NewPassword = NewPasswordBox.Password
            };
            HttpResponseMessage response = await _http.Put(baseUrl + url, data);

            if (!response.IsSuccessStatusCode)
            {
                UserFeedback = ((int)response.StatusCode).ToString()
                    + ": " + await response.Content.ReadAsStringAsync();
                return;
            }

            PasswordFeedback = "Password updated successfully.";
        }
    }
}