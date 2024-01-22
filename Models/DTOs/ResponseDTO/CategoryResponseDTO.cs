using Models.Entities;

namespace Models.DTOs.ResponseDTO;

public record CategoryResponseDTO(int Id, string Name)
{
    public static implicit operator CategoryResponseDTO(Category category)
    {
        return new CategoryResponseDTO(
            Id: category.Id,
            Name: category.Name
            );
    }
}
