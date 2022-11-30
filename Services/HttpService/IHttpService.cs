using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;

namespace wpfSBIFS.Services.HttpService
{
    public interface IHttpService
    {
        public Task<HttpResponseMessage> Get(string url);
        public Task<HttpResponseMessage> Post(string url, IJson data);
        public Task<HttpResponseMessage> Put(string url, IJson data);
        public Task<HttpResponseMessage> Delete(string url, IJson data);
        public void AddAuthentication(string jwt);
    }
}
