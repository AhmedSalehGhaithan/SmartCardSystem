﻿namespace SmartCardLibrary.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
