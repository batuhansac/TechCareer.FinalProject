using Models.Entities;

namespace Models.DTOs.ResponseDTO;

public record CommentResponseDTO(int Id, string Content, short DatePosted, Guid UserId, int PostId)
{
    public static implicit operator CommentResponseDTO(Comment comment)
    {
        return new CommentResponseDTO(
            Id: comment.Id,
            Content: comment.Content,
            DatePosted: comment.DatePosted,
            UserId: comment.UserId,
            PostId: comment.PostId
            );
    }
}
