using EmailService.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmailService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
