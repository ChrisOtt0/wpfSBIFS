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
        string baseUrl = "UserLogin/";

        private string loginEmail = string.Empty;
        private string feedbackLabel = string.Empty;
        private readonly IHttpService _http;
        private readonly ITokenService _token;

        public LoginViewModel(IHttpService http, ITokenService token)
        {
            Login = new Command(LoginCommand);
            _http = http;
            _token = token;
        }

        public string LoginEmail
        {
            get => loginEmail;
            set
            {
                loginEmail = value;
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

        public Command Login { get; set; }

        public async Task LoginCommand()
        {
            FeedbackLabel = "Connecting...";
            string url = "Login";

            if (LoginEmail == "" || LoginEmail == string.Empty)
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
                Email = LoginEmail,
                Password = PasswordBox.Password,
            };
            HttpResponseMessage response = await _http.Post(baseUrl + url, data);

            switch( (int) response.StatusCode )
            {
                // OK
                case 200:
                    TokenDto json = await response.Content.ReadFromJsonAsync<TokenDto>();
                    _token.Jwt = json.Jwt;
                    _http.AddAuthentication(_token.Jwt);

                    ((App)App.Current).ChangeUserControl(App.container.Resolve<NavMenuView>());
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
