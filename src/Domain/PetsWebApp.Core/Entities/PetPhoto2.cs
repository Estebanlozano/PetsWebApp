using System.ComponentModel.DataAnnotations;

namespace PetsWebApp.Core.Entities
{
    public class PetPhoto2 : AuditableEntity
    {
        [Required]
        public string? Path { get; set; }
        public int PetId { get; set; }
        public virtual Pet? Pet { get; set; }
    }
}
