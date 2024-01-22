namespace Models.DTOs.ResponseDTO;

public record PostDetailDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public short DatePosted { get; init; }
    public string UserName { get; init; }
    public string CategoryName { get; init; }
}