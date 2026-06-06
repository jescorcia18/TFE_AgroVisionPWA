using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeePestDetection.Domain.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;

        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;

        [Column("full_name")]
        public string FullName { get; set; } = null!;

        public string Role {  get; set; } = null!;

        [Column("organization_id")]
        public Guid OrganizationId { get; set; }

        // navegación
        public Organization Organization { get; set; } = null!;
    }
}
