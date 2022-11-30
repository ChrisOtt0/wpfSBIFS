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
        private readonly HttpService _httpService;
        private readonly TokenService _tokenService;
        

        private string name = string.Empty;
        private string email = string.Empty;
        private string baseUrl = "User/";
        private Command SaveChanges { get; set; }
        private Command UpdatePassword { get; set; }
        public PasswordBox OldPasswordBox { get; set; }
        public PasswordBox NewPasswordBox { get; set; }
        public PasswordBox NewPasswordAgainBox { get; set; }

        private string AccountName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string AccountEmail
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel(HttpService httpService, TokenService tokenService)
        {
            _httpService = httpService;
            _tokenService = tokenService;
            SaveChanges = new Command(SaveChangesCommand);
            UpdatePassword = new Command(UpdatePasswordCommand);
        }

        private async Task SaveChangesCommand()
        {
            string url = "UpdateUser";
            IJson data = new UserDto() {Name = AccountName, Email = AccountEmail};
            var result = await _httpService.Put(baseUrl + url, data );

                MessageBox.Show(result.IsSuccessStatusCode ? "Account updated successfully" : "Account update failed");
            
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