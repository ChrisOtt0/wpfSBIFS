using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
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
        private string baseUrl = "User/";
        private readonly IHttpService _httpService;

        public Command SaveChanges { get; set; }
        public Command UpdatePassword { get; set; }
        public PasswordBox OldPasswordBox { get; set; }
        public PasswordBox NewPasswordBox { get; set; }
        public PasswordBox NewPasswordAgainBox { get; set; }

        public string AccountName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string AccountEmail
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel(IHttpService httpService)
        {
            SaveChanges = new Command(SaveChangesCommand);
            UpdatePassword = new Command(UpdatePasswordCommand);
            _httpService = httpService;
        }

        private async Task SaveChangesCommand()
        {
            string url = "UpdateUser";
            IJson data = new UserDto() {Name = AccountName, Email = AccountEmail};
            var result = await _httpService.Put(baseUrl + url, data );
            string message = result.IsSuccessStatusCode ? "Changes saved" : "Something went wrong";
            message += $"\n{result.StatusCode}";
            message += $"\n{await result.Content.ReadAsStringAsync()}";
                MessageBox.Show(message);
            
        }

        private async Task UpdatePasswordCommand() 
        {
            string url = "UpdatePassword";
            if (OldPasswordBox.Password == string.Empty || NewPasswordBox.Password == string.Empty || NewPasswordAgainBox.Password == string.Empty)
            {
                MessageBox.Show("Please fill in all fields"); //Replace with trigger message to the right of the field or something like that
            }
            else if (NewPasswordBox.Password != NewPasswordAgainBox.Password)
            {
                MessageBox.Show("New passwords do not match"); //Again message which displays to the right of the field or something like that
            }
            else
            {
                IJson data = new PasswordDto() {OldPassword = OldPasswordBox.Password, NewPassword = NewPasswordBox.Password};
                var response = await _httpService.Put(baseUrl + url, data);
                
                MessageBox.Show(response.IsSuccessStatusCode ? "Password updated" : "Failed to update password"); 
            }
        }
    }
}
/*private async Task UpdatePasswordCommand()
{
Checking if the old password is the same as the current password and if the new password is the same as the
new password again. If both are true, it will change the current password. 
 if (OldPasswordBox.Password == _tokenService.Jwt && NewPasswordBox.Password == NewPasswordAgainBox.Password)
 {
     _tokenService.Jwt = NewPasswordBox.Password;
 }*/