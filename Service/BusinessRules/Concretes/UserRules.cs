using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.BusinessRules.Concretes;

public class UserRules : IUserRules
{
    private readonly IUserRepository _userRepository;

    public UserRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public void UserEmailMustBeUnique(string email)
    {
        var user = _userRepository.GetByFilter(x => x.Email == email);

        if (user != null)
        {
            throw new BusinessException("Kullanıcı e-mail adresi benzersiz olmalıdır.");
        }
    }

    public void UserEmailMustBeValid(string email)
    {
        var user = _userRepository.GetByFilter(x => x.Email == email);

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessException("Geçerli bir e-mail adresi girilmelidir.");
        }
    }

    public void UserIsPresent(Guid id)
    {
        var user = _userRepository.GetById(id);

        if (user == null)
        {
            throw new BusinessException($"ID değeri {id} olan bir kullanıcı bulunamadı.");
        }
    }

    public void UserNameMustBeUnique(string userName)
    {
        var user = _userRepository.GetByFilter(x => x.UserName == userName);

        if (user != null)
        {
            throw new BusinessException("Kullanıcı adı benzersiz olmalıdır.");
        }
    }

    public void UserNameMustBeValid(string userName)
    {
        var user = _userRepository.GetByFilter(x => x.UserName == userName);

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new BusinessException("Geçerli bir kullanıcı adı girilmelidir.");
        }
    }
}
