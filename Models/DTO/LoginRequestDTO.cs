﻿namespace BPKBBackend.Models
{
    public class LoginRequestDTO
    {
        public string Username{ get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
    }

}
