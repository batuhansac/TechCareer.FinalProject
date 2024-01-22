using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.BusinessRules.Concretes;

public class CommentRules : ICommentRules
{
    private readonly ICommentRepository _commentRepository;

    public CommentRules(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public void CommentContentMustBeValid(string content)
    {
        var comment = _commentRepository.GetByFilter(x => x.Content == content);

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new BusinessException("İçerik boş olamaz ya da boşluklardan oluşamaz.");
        }
    }

    public void CommentIsPresent(int id)
    {
        var comment = _commentRepository.GetById(id);

        if (comment == null)
        {
            throw new BusinessException($"ID değeri {id} olan bir yorum bulunamadı.");
        }
    }
}
