namespace GuestBookRazorPages.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryMessage Messages { get; }
        IRepositoryUser Users { get; }
        Task Save();
    }
}
