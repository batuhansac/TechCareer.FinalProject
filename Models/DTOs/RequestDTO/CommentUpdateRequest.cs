using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record CommentUpdateRequest(int Id, string Content, short DatePosted, Guid UserId, int PostId)
{
    public static implicit operator Comment(CommentUpdateRequest commentUpdateRequest)
    {
        return new Comment
        {
            Id = commentUpdateRequest.Id,
            Content = commentUpdateRequest.Content,
            DatePosted = commentUpdateRequest.DatePosted,
            UserId = commentUpdateRequest.UserId,
            PostId = commentUpdateRequest.PostId
        };
    }
}
