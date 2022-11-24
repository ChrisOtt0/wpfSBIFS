using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Model
{
    public class ConnectionBuilder
    {
        TcpClient tcp;

        string _hostName;
        int _port;



        public void AddHostname (string hostName)
        {
            _hostName = hostName;
        }


        public void AddPort (int port)
        {
            _port = port;
        }


        public async Task<TcpClient> Connect()
        {
            //if no values, insert default
            if (_hostName.Equals("")) _hostName = "127.0.0.1";
            if (_port == null) _port = 8080;

            //make new connection
            tcp = new TcpClient (_hostName, _port);
            return tcp;
        }


    }
}
