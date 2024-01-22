namespace Service.BusinessRules.Abstracts;

public interface ICommentRules
{
    void CommentContentMustBeValid(string content);
    void CommentIsPresent(int id);
}
