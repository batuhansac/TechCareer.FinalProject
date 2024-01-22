using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record CategoryUpdateRequest(int Id, string Name)
{
    public static implicit operator Category(CategoryUpdateRequest categoryUpdateRequest)
    {
        return new Category
        {
            Id = categoryUpdateRequest.Id,
            Name = categoryUpdateRequest.Name,
        };
    }
}
