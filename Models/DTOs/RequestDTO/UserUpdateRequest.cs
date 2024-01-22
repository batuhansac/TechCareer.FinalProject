using System.Net.Mail;
using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record UserUpdateRequest(Guid Id, string UserName, string Email)
{
    public static implicit operator User(UserUpdateRequest userUpdateRequest)
    {
        return new User
        {
            Id = userUpdateRequest.Id,
            UserName = userUpdateRequest.UserName,
            Email = userUpdateRequest.Email
        };
    }
}
