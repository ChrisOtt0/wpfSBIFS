﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Model
{
    public class RegisterJson : IJson
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}