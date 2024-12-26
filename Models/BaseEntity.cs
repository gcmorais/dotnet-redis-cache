namespace project_cache.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset? DateUpdated { get; private set; }
        public DateTimeOffset? DateDeleted { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTimeOffset.UtcNow;
        }

        public void UpdateDate()
        {
            DateUpdated = DateTimeOffset.UtcNow;
        }

        public void MarkAsDeleted()
        {
            if (DateDeleted == null)
            {
                DateDeleted = DateTimeOffset.UtcNow;
            }
        }
    }
}
