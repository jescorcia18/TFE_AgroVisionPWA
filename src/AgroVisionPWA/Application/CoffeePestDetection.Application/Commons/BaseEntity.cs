using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Application.Commons
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        [Column("created_At")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_At")]

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
