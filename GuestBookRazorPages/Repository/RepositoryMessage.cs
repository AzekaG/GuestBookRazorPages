using GuestBookRazorPages.EF;
using GuestBookRazorPages.Interfaces;
using GuestBookRazorPages.Model;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.Repository
{
    public class RepositoryMessage : IRepositoryMessage
    {
        MessageUserContext _messageUserContext;

        public RepositoryMessage(MessageUserContext messageUserContext)
        {
            _messageUserContext = messageUserContext;
        }


        public async Task<List<Message>> GetMessageList()
        {
            return await _messageUserContext.Messages.Include(x => x.Us).ToListAsync();
        }
    }
}
