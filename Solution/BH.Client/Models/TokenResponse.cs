﻿using System;

namespace BH.Client.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
