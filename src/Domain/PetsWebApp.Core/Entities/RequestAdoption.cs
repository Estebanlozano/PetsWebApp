namespace PetsWebApp.Core.Entities
{
    public class RequestAdoption: AuditableEntity
    {
        public Guid UserId { get; set; }

        public int PetId { get; set; }
        public string? CoverLetter { get; set; }
        public string? Response { get; set; }
        public bool? Approved { get; set; }
    }
}
