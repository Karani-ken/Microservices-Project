﻿namespace EmailService.Model
{
    public class EmailLogger
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
