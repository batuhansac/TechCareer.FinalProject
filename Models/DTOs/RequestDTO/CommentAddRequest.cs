using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record CommentAddRequest(string Content, short DatePosted, Guid UserId, int PostId)
{
    public static implicit operator Comment(CommentAddRequest commentAddRequest)
    {
        return new Comment
        {
            Content = commentAddRequest.Content,
            DatePosted = commentAddRequest.DatePosted,
            UserId = commentAddRequest.UserId,
            PostId = commentAddRequest.PostId
        };
    }
}