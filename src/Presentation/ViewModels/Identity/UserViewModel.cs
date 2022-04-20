
namespace Presentation.ViewModels.Identity
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? NameSurname { get; set; }

        public IEnumerable<string> Claims { get; set; } = new string[0];

        public IEnumerable<string> Roles { get; set; } = new string[0];

        public DateTime CreatedAt { get; set; }
    }
}
