using System.Net.Mail;
using Models.Entities;

namespace Models.DTOs.ResponseDTO;

public record UserResponseDTO(Guid Id, string UserName, string Email)
{
    public static implicit operator UserResponseDTO(User user)
    {
        return new UserResponseDTO(
            Id: user.Id,
            UserName: user.UserName,
            Email: user.Email
            );
    }
}
