using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record PostAddRequest(string Title, string Content, short DatePosted, Guid UserId, int CategoryId)
{
    public static implicit operator Post(PostAddRequest postAddRequest)
    {
        return new Post
        {
            Title = postAddRequest.Title,
            Content = postAddRequest.Content,
            DatePosted = postAddRequest.DatePosted,
            UserId = postAddRequest.UserId,
            CategoryId = postAddRequest.CategoryId
        };
    }
}
