using System.ComponentModel.DataAnnotations;
using Domain.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        [MaxLength(100)]
        public string? NameSurname { get; set; }

        [MaxLength(150)]
        public string? ImageUrl { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }
    }
}