using System.ComponentModel.DataAnnotations;

namespace PetsWebApp.Core.Entities
{
    public class UserProfile : AuditableEntity
    {

        [Required]  
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string? Phone {get; set; }
      
        public string? Avatar { get; set; }
        public int PetsPublished { get; set; }
        public int PetsAdopted { get; set; }
    }
}
