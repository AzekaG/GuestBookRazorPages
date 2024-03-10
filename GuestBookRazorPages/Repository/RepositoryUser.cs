using GuestBookRazorPages.EF;
using GuestBookRazorPages.Interfaces;
using GuestBookRazorPages.Model;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        readonly MessageUserContext _messageUserContext;

        public RepositoryUser(MessageUserContext messageUserContext)
        {
            _messageUserContext = messageUserContext;
        }

        public async Task<List<User>> GetUserList()
        {
            return await _messageUserContext.Users.ToListAsync();
        }

        public async Task CreateUser(User user)
        {
            await _messageUserContext.Users.AddAsync(user);
            await Save();


        }
        public async Task Save()
        {
            await _messageUserContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _messageUserContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                _messageUserContext.Users.Remove(user);
            await Save();
        }

        public async Task AddMessage(Message message)
        {
            var us = await _messageUserContext.Users.FindAsync(message.Us.Id);
            if (us != null)
            { 
                us.messages!.Add(message);
            }
            await Save();
        }
    }
}
