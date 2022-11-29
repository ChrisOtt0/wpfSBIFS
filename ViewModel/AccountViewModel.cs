using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity.Injection;
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
            var response = await _httpService.Put("api/account", new
            {
                name = AccountName,
                email = AccountEmail
            });
            MessageBox.Show(response.IsSuccessStatusCode ? "Account updated" : "Failed to update account");
        }

        private async Task UpdatePasswordCommand()
        {
            var response = await _httpService.Put("api/account/update/password", new
            {
                oldPassword = OldPasswordBox.Password,
                newPassword = NewPasswordBox.Password,
                newPasswordAgain = NewPasswordAgainBox.Password
            }, _tokenService.Jwt);

            MessageBox.Show(response.IsSuccessStatusCode ? "Password updated" : "Failed to update password");
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