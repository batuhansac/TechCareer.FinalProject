namespace Service.BusinessRules.Abstracts;

public interface ICategoryRules
{
    void CategoryNameMustBeUnique(string categoryName);
    void CategoryNameMustBeValid(string categoryName);
    void CategoryIsPresent(int id);
}