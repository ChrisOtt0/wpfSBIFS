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
    public class GroupViewModel : Bindable, IGroupViewModel
    {
        private readonly HttpService _httpService;
        private readonly TokenService _tokenService;

        public GroupViewModel(HttpService httpService, TokenService tokenService)
        {
            _httpService = httpService;
            _tokenService = tokenService;
        }


    }
}
