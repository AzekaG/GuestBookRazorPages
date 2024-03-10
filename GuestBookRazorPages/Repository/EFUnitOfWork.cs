using GuestBookRazorPages.EF;
using GuestBookRazorPages.Interfaces;

namespace GuestBookRazorPages.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        MessageUserContext messageUserContext;
        RepositoryMessage messageRepo;
        RepositoryUser userRepo;


        public EFUnitOfWork(MessageUserContext messageUserContext)
        {
            try { this.messageUserContext = messageUserContext; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public IRepositoryUser Users
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new RepositoryUser(messageUserContext);
                }
                return userRepo;
            }
        }

        public IRepositoryMessage Messages
        {
            get
            {
                if (messageRepo == null)
                {
                    messageRepo = new RepositoryMessage(messageUserContext);
                }
                return messageRepo;
            }
        }

        public async Task Save()
        {
            await messageUserContext.SaveChangesAsync();
        }

    }
}
