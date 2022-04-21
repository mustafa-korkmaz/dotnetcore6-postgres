
namespace Application.Dto.Identity
{
    public class UserDto : DtoBase<Guid>
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? NameSurname { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<string> Claims { get; set; } = new List<string>();

        public DateTimeOffset CreatedAt { get; set; }

    }
}
