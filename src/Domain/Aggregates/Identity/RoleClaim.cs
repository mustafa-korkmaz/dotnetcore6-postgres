
namespace Domain.Aggregates.Identity
{
    public class RoleClaim : EntityBase<Guid>
    {
        public int RoleId { get; private set; }

        public Role? Role { get; private set; }

        public int ClaimId { get; private set; }
        public Claim? Claim { get; private set; }

        public RoleClaim(Guid id, int roleId, int claimId) : base(id)
        {
            ClaimId = claimId;
            RoleId = roleId;
        }
    }
}
