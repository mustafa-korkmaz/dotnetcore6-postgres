using Domain.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<System.Guid>, IEntity<Guid>
    {
    }
}
