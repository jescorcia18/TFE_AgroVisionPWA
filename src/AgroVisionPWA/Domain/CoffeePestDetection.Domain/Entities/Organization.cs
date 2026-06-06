using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities;

public class Organization
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string type { get; set; } = string.Empty;

    public string? Nit { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Profile> Profiles { get; set; }
        = new List<Profile>();
}
