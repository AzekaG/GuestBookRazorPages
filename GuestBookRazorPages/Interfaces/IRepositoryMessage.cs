using GuestBookRazorPages.Model;

namespace GuestBookRazorPages.Interfaces
{
    public interface IRepositoryMessage
    {
        Task<List<Message>> GetMessageList();
    }
}
