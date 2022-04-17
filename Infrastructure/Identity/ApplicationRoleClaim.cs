using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRoleClaim<T> : IdentityRoleClaim<T> where T : IEquatable<T>
    {
    }
}
