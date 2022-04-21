
namespace Presentation.ViewModels.Identity
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? NameSurname { get; set; }

        public IEnumerable<string> Claims { get; set; } = Array.Empty<string>();

        public DateTime CreatedAt { get; set; }
    }
}
