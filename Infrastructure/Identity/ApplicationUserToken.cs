using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserToken<T> : IdentityUserToken<T> where T : IEquatable<T>
    {
    }
}
