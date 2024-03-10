using GuestBookRazorPages.Model;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.EF
{
    public class MessageUserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public MessageUserContext(DbContextOptions<MessageUserContext> options) : base(options)
        {

            if (Database.EnsureCreated())
            {
                var admin = new User { Login = "admin" , Password = "81BE1E9142602690FDFEE07B7477A64C", Salt = "3158E91A852E3ED3F159BEC2F2AB73C7", Email = "e@gmail.com"};
                Users.Add(admin);
                
                Messages.Add(new Message { Us = admin, date = DateTime.Now, msg = "My First FeedBack" });
                Messages.Add(new Message { Us = admin, date = DateTime.Now, msg = "My Second FeedBack" });
                Messages.Add(new Message { Us = admin, date = DateTime.Now, msg = "My Fird FeedBack" });

                SaveChanges();


            };
        }
    }
}
