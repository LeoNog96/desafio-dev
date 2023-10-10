namespace Cnab.Api.Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Removed = false;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public bool Removed { get; set; }
    }
}
