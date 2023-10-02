using System.ComponentModel.DataAnnotations;

namespace PetsWebApp.Web.ViewModels
{
    public class RequestAdoptionViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int PetId { get; set; }

        [MaxLength(1000)]
        [Required]
        public string? CoverLetter { get; set; }
        public string? Response { get; set; }
        public bool Approved { get; set; }
    }
}
