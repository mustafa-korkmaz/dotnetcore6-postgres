using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserClaim<T> : IdentityUserClaim<T> where T : IEquatable<T>
    {
    }
}
