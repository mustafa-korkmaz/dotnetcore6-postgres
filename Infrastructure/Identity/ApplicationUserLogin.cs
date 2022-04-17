using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserLogin<T> : IdentityUserLogin<T> where T : IEquatable<T>
    {
    }
}
