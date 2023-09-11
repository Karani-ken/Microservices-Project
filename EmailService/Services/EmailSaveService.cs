using EmailService.Data;
using EmailService.Model;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Services
{
    public class EmailSaveService
    {
        private DbContextOptions<ApplicationDbContext> options;
        
        public EmailSaveService(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }
        public async Task SaveData(EmailLogger emailLogger)
        {
            var _context = new ApplicationDbContext(this.options);
            _context.EmailLoggers.Add(emailLogger);
            await _context.SaveChangesAsync();
        }
    }
}
