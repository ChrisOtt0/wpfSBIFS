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
        private readonly IHttpService _http;
        private readonly ITokenService _token;

        public GroupViewModel(IHttpService http, ITokenService token)
        {
            _http = http;
            _token = token;
        }


    }
}
