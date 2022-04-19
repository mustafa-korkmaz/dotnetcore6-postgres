
namespace Domain.Aggregates.Identity
{
    public class UserRole : EntityBase<Guid>
    {
        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public int RoleId { get; private set; }
        public Role? Role { get; private set; }

        public UserRole(Guid id, Guid userId, int roleId) : base(id)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
