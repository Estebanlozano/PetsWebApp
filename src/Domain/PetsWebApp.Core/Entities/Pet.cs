using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsWebApp.Core.Entities
{
    public class Pet : AuditableEntity
    {
        [Required]
        [MaxLength(100)]
        public string ? Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public bool Castrated { get; set; }

        public Guid? UserId { get; set; }

        [InverseProperty("Pet")]
        public virtual List<PetPhoto2>? Photos { get; set; }
    }
}
