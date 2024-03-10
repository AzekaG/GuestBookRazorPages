using GuestBookRazorPages.Model;

namespace GuestBookRazorPages.Interfaces
{
    public interface IRepositoryUser
    {
        Task<List<User>> GetUserList();

        Task CreateUser(User user);

        Task Save();

        Task DeleteUser(int id);

        Task AddMessage(Message message);
    }
}
