using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.BusinessRules.Concretes;

public class PostRules : IPostRules
{
    private readonly IPostRepository _postRepository;

    public PostRules(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public void PostContentMustBeValid(string content)
    {
        var post = _postRepository.GetByFilter(x => x.Content == content);

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new BusinessException("Gönderinin içeriği boş olamaz ya da sadece boşluklardan oluşamaz.");
        }
    }

    public void PostIsPresent(int id)
    {
        var post = _postRepository.GetById(id);

        if (post == null)
        {
            throw new BusinessException($"ID değeri {id} olan bir gönderi bulunamadı.");
        }
    }

    public void PostTitleMustBeValid(string title)
    {
        var post = _postRepository.GetByFilter(x => x.Title == title);

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new BusinessException("Gönderinin başlığı boş olamaz ya da sadece boşluklardan oluşamaz.");
        }
    }
}
