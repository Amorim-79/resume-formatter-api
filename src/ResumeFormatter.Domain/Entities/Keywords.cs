namespace ResumeFormatter.Domain.Entities
{
    public class Keyword : BaseEntity
    {
        public required int UserId { get; set; }
        public required string Word { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
