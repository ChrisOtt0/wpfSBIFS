using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public AccountViewModel(HttpService httpService, TokenService tokenService)
        {
            _httpService = httpService;
            _tokenService = tokenService;
            SaveChanges = new Command(SaveChangesCommand);
            UpdatePassword = new Command(UpdatePasswordCommand);
        }

        private async Task SaveChangesCommand()
        {
            MessageBox.Show("Name: " + name + "\nEmail: " + email);
        }

        private async Task UpdatePasswordCommand()
        {
            MessageBox.Show("Old: " + OldPasswordBox.Password + "\nNew: " + NewPasswordBox.Password + "\nNew Again: " + NewPasswordAgainBox.Password);
        }
    }
}
