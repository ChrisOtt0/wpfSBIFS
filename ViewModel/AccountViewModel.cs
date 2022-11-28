using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public AccountViewModel(HttpService httpService, TokenService tokenService)
        {
            _httpService = httpService;
            _tokenService = tokenService;
            SaveChanges = new Command(SaveChangesCommand);
        }

        private async Task SaveChangesCommand()
        {

        }
    }
}
