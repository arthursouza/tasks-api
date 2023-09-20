namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public BaseEntity()
        {
            CreatedAt = LastUpdatedAt = DateTime.UtcNow;
        }
    }
}