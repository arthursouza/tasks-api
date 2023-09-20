using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class WorkItem : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }

    internal void Update(WorkItem entity)
    {
        LastUpdatedAt = DateTime.UtcNow;
        Title = entity.Title;
        Description = entity.Description;
    }
}
