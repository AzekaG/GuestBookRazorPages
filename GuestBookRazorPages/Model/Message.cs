using System.ComponentModel.DataAnnotations;

namespace GuestBookRazorPages.Model
{
    public class Message
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Отзыв не может быть пустым")]

        public string? msg { get; set; }

        public DateTime? date { get; set; }

        public User Us { get; set; }

    }
}
