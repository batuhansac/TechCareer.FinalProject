namespace Models.DTOs.ResponseDTO;

public record CommentDetailDTO
{
    public int Id { get; init; }
    public string Content { get; init; }
    public short DatePosted { get; init; }
    public string UserName { get; init; }
    public string PostTitle { get; init; }
}