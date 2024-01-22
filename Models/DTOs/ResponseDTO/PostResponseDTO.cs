using Models.Entities;

namespace Models.DTOs.ResponseDTO;

public record PostResponseDTO(int Id, string Title, string Content, short DatePosted, Guid UserId, int CategoryId)
{
    public static implicit operator PostResponseDTO(Post post)
    {
        return new PostResponseDTO(
            Id: post.Id,
            Title: post.Title,
            Content: post.Content,
            DatePosted: post.DatePosted,
            UserId: post.UserId,
            CategoryId: post.CategoryId
            );
    }
}
