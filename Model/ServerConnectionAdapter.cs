using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Xml.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using wpfSBIFS.Tools;

namespace wpfSBIFS.Model { 

    public class ServerConnectionAdapter
    {
    
        TcpClient _client;
        NetworkStream _ns;
        StreamReader _reader;
        HttpClient client;

        string _HostName;
        int _Port;
        string jwt;

      
        public ServerConnectionAdapter(string HostName, int Port)
        {
            _HostName = HostName;
            _Port = Port;
            
            connect();
        }


        private async Task connect()
        {
            //build new connection
            ConnectionBuilder cb = new ConnectionBuilder();
            cb.AddHostname(_HostName);
            cb.AddPort(_Port);

            //connect 
            try
            {
                _client = await cb.Connect();
                _ns = _client.GetStream();
                _reader = new StreamReader(_ns, Encoding.UTF8);
                client = new HttpClient();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("something went wrong: \n\n" + ex.Message);
            }

        }



        public async Task Login(string User, string Password)
        {

            //error handling for if user or password is empty 
            if (Util.CheckUsernamePassword(User, Password)) MessageBox.Show("Please enter username & password!");

            //attaching user and password to login json 
            IJson loginJson = new LoginJson
            {
                Email = User,
                Password = Password,
            };

            //making the login request
            var response = await client.PostAsJsonAsync("https://localhost:8080/Api/Auth/Login", loginJson);

            //defining the cariable statuscode which was extracted from the request response
            var StatusCode = (Int32)response.StatusCode;

            //Checking the statuscode for all kinds of error statuscodes
            if (Util.CheckStatusCode(StatusCode) != "")
            {
                MessageBox.Show((Util.CheckStatusCode(StatusCode)));
                return;
            }

            //parsing the response text 
            JObject json = JObject.Parse(response.Content.ToString());

            //getting the jwt login token from response text
            jwt = (string)json["jwt"];


        }
        public async Task Register(string User, string Password)
        {

            //error handling for if user or password is empty 
            if (Util.CheckUsernamePassword(User, Password)) MessageBox.Show("Please enter username & password!");

            //attaching user and password to register json 
            IJson registerJson = new RegisterJson
            {
                Email = User,
                Password = Password,
            };

            //making the login request
            var response = await client.PostAsJsonAsync("https://localhost:8080/Api/Auth/Register", registerJson);

            //defining the cariable statuscode which was extracted from the request response
            var StatusCode = (Int32)response.StatusCode;

            //Checking the statuscode for all kinds of error statuscodes
            if (Util.CheckStatusCode(StatusCode) != "")
            {
                MessageBox.Show((Util.CheckStatusCode(StatusCode)));
                return;
            }

            //parsing the response text 
            JObject json = JObject.Parse(response.Content.ToString());

            //getting the jwt login token from response text
            jwt = (string)json["jwt"];


        }





        

    }
}
