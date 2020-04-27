﻿using System;
using Jinder.Poco.Type;

namespace Jinder.Poco.Model
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String PasswordHash { get; set; }
        public UserType Type { get; set; }
    }
}