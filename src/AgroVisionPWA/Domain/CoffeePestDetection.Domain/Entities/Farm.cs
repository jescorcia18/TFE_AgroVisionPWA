using CoffeePestDetection.Application.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Entities
{
    public class Farm : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Location { get; set; }

        public decimal? AreaHectares { get; set; }

        public bool IsActive { get; set; } = true;
        /** FK */
        [Column("organization_id")]
        public Guid OrganizationId { get; set; }


        /** Navigation*/

        public Organization Organization { get; set; } = null!;

        public ICollection<Plot> Plots { get; set; } = new List<Plot>();
    }
}
