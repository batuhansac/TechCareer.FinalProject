using System.Net.Mail;

namespace Service.BusinessRules.Abstracts;

public interface IUserRules
{
    void UserNameMustBeUnique(string userName);
    void UserNameMustBeValid(string userName);
    void UserEmailMustBeUnique(string email);
    void UserEmailMustBeValid(string email);
    void UserIsPresent(Guid id);
}
