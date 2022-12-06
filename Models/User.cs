﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;

namespace wpfSBIFS.Models
{
    public enum Privileges
    {
        admin,
        user
    }

    public class User : IJson
    {
        public int UserID { get; set; }
        public string? Name { get; set; } = string.Empty;
        public Privileges? Privilege { get; set; } = Privileges.user;
        public List<Group>? Groups { get; set; } = new List<Group>();
        public List<Activity>? Activities { get; set; } = new List<Activity>();
    }
}
