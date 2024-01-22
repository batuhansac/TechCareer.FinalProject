using Core.Persistence.EntityBaseModel;

namespace Models.Entities;

public class Post : Entity<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public short DatePosted { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<Comment> Comments { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
