﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.Services.TokenService
{
    public interface ITokenService
    {
        public string Jwt { get; set; }
    }
}
