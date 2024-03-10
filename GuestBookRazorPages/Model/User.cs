namespace GuestBookRazorPages.Model
{
    public class User
    {
        public int Id { get; set; }

        public string? Login { get; set; }
 
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }

        public ICollection<Message>? messages { get; set; }

        public User()
        {
            messages = new HashSet<Message>();
        }
    }
}
