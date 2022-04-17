using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserRole<T> : IdentityUserRole<T> where T : IEquatable<T>
    {
    }
}
