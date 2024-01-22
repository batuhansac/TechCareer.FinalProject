using Core.Persistence.EntityBaseModel;

namespace Models.Entities;

public class Comment : Entity<int>
{
    public string Content { get; set; }
    public short DatePosted { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
}
