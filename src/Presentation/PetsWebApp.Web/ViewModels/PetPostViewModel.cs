using System.ComponentModel.DataAnnotations;

namespace PetsWebApp.Web.ViewModels
{
    public class PetViewModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public int Age { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public bool Castrated { get; set; }

        public IEnumerable<string>? Photos { get; set; }
        public Guid? UserId { get; internal set; }
    }

    public class PetPostViewModel
    {
        public int Id { get; set; }
        public int PetPostId { get; set; }
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public int Age { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public bool Castrated { get; set; }

        [Required]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? PetPostDescription { get; set; }

        public List<string>? Photos { get; set; }
    }
}
