using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record CategoryAddRequest(string Name)
{
    public static implicit operator Category(CategoryAddRequest categoryAddRequest)
    {
        return new Category
        {
            Name = categoryAddRequest.Name,
        };
    }
}
