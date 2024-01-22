namespace Service.BusinessRules.Abstracts;

public interface IPostRules
{
    void PostTitleMustBeValid(string title);
    void PostContentMustBeValid(string content);
    void PostIsPresent(int id);
}
