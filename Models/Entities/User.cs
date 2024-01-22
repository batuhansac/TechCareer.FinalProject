using System.Net.Mail;
using Core.Persistence.EntityBaseModel;

namespace Models.Entities;

public class User : Entity<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }

    public List<Post> Posts { get; set; }
    public List<Comment> Comments { get; set; }
}