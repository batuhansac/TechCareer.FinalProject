using System.Net.Mail;
using Models.Entities;

namespace Models.DTOs.RequestDTO;

public record UserAddRequest(string UserName, string Email)
{
    public static implicit operator User(UserAddRequest userAddRequest)
    {
        return new User
        {
            UserName = userAddRequest.UserName,
            Email = userAddRequest.Email
        };
    }
}
