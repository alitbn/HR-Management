﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Management.Application.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
