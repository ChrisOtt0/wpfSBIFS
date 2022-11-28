using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public class LoginViewModel : Bindable, ILoginViewModel
    {
        private readonly HttpService _httpService;
        private readonly TokenService _tokenService;
        string baseUrl = "UserLogin/";

        public string loginFormEmail = string.Empty;
        public string feedbackLabel = string.Empty;

        public LoginViewModel(HttpService httpService, TokenService tokenService)
        {
            LoginCommand = new Command(Login);
            _httpService = httpService;
            _tokenService = tokenService;
        }

        public string LoginFormEmail
        {
            get => loginFormEmail;
            set
            {
                loginFormEmail = value;
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

        public PasswordBox? PasswordBox { get; set; }

        public Command LoginCommand { get; set; }

        public async Task Login()
        {
            FeedbackLabel = "Connecting...";
            string url = "Login";

            if (LoginFormEmail == "" || LoginFormEmail == string.Empty)
            {
                FeedbackLabel = "Please enter an email.";
                return;
            }

            if (PasswordBox.Password == "" || PasswordBox.Password == string.Empty)
            {
                FeedbackLabel = "No password given.";
                return;
            }

            IJson data = new UserLoginDto
            {
                Email = LoginFormEmail,
                Password = PasswordBox.Password,
            };
            HttpResponseMessage response = await _httpService.Post(baseUrl + url, data);

            switch( (int) response.StatusCode )
            {
                // OK
                case 200:
                    TokenDto json = await response.Content.ReadFromJsonAsync<TokenDto>();
                    _tokenService.Jwt = json.Jwt;

                    ((App)App.Current).ChangeUserControl(App.container.Resolve<GroupView>());
                    break;

                // BadRequest
                case 400:
                // Unauthorized
                case 401:
                    FeedbackLabel = await response.Content.ReadAsStringAsync();
                    break;

                // Server Error
                case 500:
                    FeedbackLabel = "Server Error. Check logs at: {log_path}";
                    break;

                default:
                    FeedbackLabel = ((int)response.StatusCode).ToString() + ": " + await response.Content.ReadAsStringAsync();
                    break;
            }
        }
    }
}
