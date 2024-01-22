using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Service.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRules _userRules;

    public UserService(IUserRepository userRepository, IUserRules userRules)
    {
        _userRepository = userRepository;
        _userRules = userRules;
    }
    public Response<UserResponseDTO> Add(UserAddRequest userAddRequest)
    {
        try
        {
            User user = userAddRequest;

            _userRules.UserNameMustBeUnique(user.UserName);
            _userRules.UserNameMustBeValid(user.UserName);
            _userRules.UserEmailMustBeUnique(user.Email);
            _userRules.UserEmailMustBeValid(user.Email);

            _userRepository.Add(user);

            UserResponseDTO userResponseDTO = user;

            return new Response<UserResponseDTO>()
            {
                Data = userResponseDTO,
                Message = "Kullanıcı başarıyla oluşturuldu.",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<UserResponseDTO> Delete(Guid id)
    {
        try
        {
            _userRules.UserIsPresent(id);

            User user = _userRepository.GetById(id);

            _userRepository.Delete(user);

            UserResponseDTO userResponseDTO = user;

            return new Response<UserResponseDTO>()
            {
                Data = userResponseDTO,
                Message = "Kullanıcı başarıyla silindi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<UserResponseDTO>> GetAll()
    {
        List<User> users = _userRepository.GetAll();

        List<UserResponseDTO> userResponseDTOs = users.Select(u => (UserResponseDTO)u).ToList();

        return new Response<List<UserResponseDTO>>()
        {
            Data = userResponseDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<UserResponseDTO> GetById(Guid id)
    {
        try
        {
            _userRules.UserIsPresent(id);

            User? user = _userRepository.GetById(id);

            UserResponseDTO userResponseDTO = user;

            return new Response<UserResponseDTO>()
            {
                Data = userResponseDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<UserResponseDTO> Update(UserUpdateRequest userUpdateRequest)
    {
        try
        {
            User user = userUpdateRequest;
          
            _userRules.UserNameMustBeValid(user.UserName);
            _userRules.UserEmailMustBeValid(user.Email);

            _userRepository.Update(user);

            UserResponseDTO userResponseDTO = user;

            return new Response<UserResponseDTO>()
            {
                Data = userResponseDTO,
                Message = "Kullanıcı başarıyla güncellendi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
