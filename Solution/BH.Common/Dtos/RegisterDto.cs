﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BH.Common.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
