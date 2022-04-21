namespace Domain.Aggregates.Identity
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IReadOnlyCollection<RoleClaim>> GetClaimsAsync(int[] roleIds);
    }
}
