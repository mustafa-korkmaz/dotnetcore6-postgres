﻿
namespace Domain.Aggregates.Identity
{
    public class User : EntityBase<Guid>
    {
        public string Username { get; private set; }

        public string Email { get; private set; }

        public string? NameSurname { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public string PasswordHash { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        private ICollection<UserRole> _roles;
        public IReadOnlyCollection<UserRole> Roles
        {
            get => _roles.ToList();
            private set => _roles = value.ToList();
        }

        public User(Guid id, string username, string email, string? nameSurname, bool isEmailConfirmed, string passwordHash) : base(id)
        {
            _roles = new List<UserRole>();
            Username = username;
            Email = email;
            NameSurname = nameSurname;
            IsEmailConfirmed = isEmailConfirmed;
            PasswordHash = passwordHash;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
