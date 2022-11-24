using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpfSBIFS.Tools
{
    public class Util
    {
        /// <summary>
        /// it checks for different failed satuscodes and returns the corresponding error message
        /// </summary>
        /// <param name="StatusCode"></param>
        /// <returns> error message </returns>

        public static String CheckStatusCode(int StatusCode)
        {
            if (StatusCode == 401)
            {
                return "Wrong username or password!";
            }
            if (StatusCode == 403)
            {
                return "Access denied!";
            }
            if (StatusCode == 501)
            {
                return "Internal Server Error, please contact administrator!";
            }
            if (StatusCode == 400)
            {
                return "Bad request, please contact administrator!";
            }
            if (StatusCode == 503)
            {
                return "Service unavailable, please try again later!";
            }
            if ((StatusCode) != 200)
            {
                return "Unknown error" + StatusCode;
            }

            return "";


        }
        public static Boolean CheckUsernamePassword(string username,string password)
        {
            if (username.Equals("") || password.Equals(""))
            {
                return true;
            }
            return false;
        }
        
        

}
