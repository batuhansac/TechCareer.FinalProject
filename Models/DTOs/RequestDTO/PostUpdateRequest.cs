using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record PostUpdateRequest(int Id, string Title, string Content, short DatePosted, Guid UserId, int CategoryId)
{
    public static implicit operator Post(PostUpdateRequest postUpdateRequest)
    {
        return new Post
        {
            Id = postUpdateRequest.Id,
            Title = postUpdateRequest.Title,
            Content = postUpdateRequest.Content,
            DatePosted = postUpdateRequest.DatePosted,
            UserId = postUpdateRequest.UserId,
            CategoryId = postUpdateRequest.CategoryId
        };
    }
}
