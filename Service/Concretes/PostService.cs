using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Service.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.Concretes;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IPostRules _postRules;

    public PostService(IPostRepository postRepository, IPostRules postRules)
    {
        _postRepository = postRepository;
        _postRules = postRules;
    }
    public Response<PostResponseDTO> Add(PostAddRequest postAddRequest)
    {
        try
        {
            Post post = postAddRequest;

            _postRules.PostTitleMustBeValid(post.Title);
            _postRules.PostContentMustBeValid(post.Content);

            _postRepository.Add(post);

            PostResponseDTO postResponseDTO = post;

            return new Response<PostResponseDTO>()
            {
                Data = postResponseDTO,
                Message = "Gönderi başarıyla oluşturuldu.",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<PostResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<PostResponseDTO> Delete(int id)
    {
        try
        {
            _postRules.PostIsPresent(id);

            Post post = _postRepository.GetById(id);

            _postRepository.Delete(post);

            PostResponseDTO postResponseDTO = post;

            return new Response<PostResponseDTO>()
            {
                Data = postResponseDTO,
                Message = "Gönderi başarıyla silindi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<PostResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<PostResponseDTO>> GetAll()
    {
        List<Post> posts = _postRepository.GetAll();

        List<PostResponseDTO> postResponseDTOs = posts.Select(p => (PostResponseDTO)p).ToList();

        return new Response<List<PostResponseDTO>>()
        {
            Data = postResponseDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<PostResponseDTO>> GetAllByDatePosted(short dateBegin, short dateEnd)
    {
        List<Post> posts = _postRepository.GetAll(p => p.DatePosted <= dateBegin && p.DatePosted >= dateEnd);

        List<PostResponseDTO> postResponseDTOs = posts.Select(p => (PostResponseDTO)p).ToList();

        return new Response<List<PostResponseDTO>>()
        {
            Data = postResponseDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<PostDetailDTO>> GetAllDetails()
    {
        List<PostDetailDTO> postDetailDTOs = _postRepository.GetAllPostDetails();

        return new Response<List<PostDetailDTO>>()
        {
            Data = postDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<PostDetailDTO>> GetAllDetailsByCategoryId(int categoryId)
    {
        List<PostDetailDTO> postDetailDTOs = _postRepository.GetDetailsByCategoryId(categoryId);

        return new Response<List<PostDetailDTO>>()
        {
            Data = postDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<PostDetailDTO>> GetAllDetailsByUserId(Guid userId)
    {
        List<PostDetailDTO> postDetailDTOs = _postRepository.GetDetailsByUserId(userId);

        return new Response<List<PostDetailDTO>>()
        {
            Data = postDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<PostDetailDTO> GetByDetailId(int id)
    {
        try
        {
            _postRules.PostIsPresent(id);

            PostDetailDTO postDetailDTO = _postRepository.GetPostDetail(id);

            return new Response<PostDetailDTO>()
            {
                Data = postDetailDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<PostDetailDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<PostResponseDTO> GetById(int id)
    {
        try
        {
            _postRules.PostIsPresent(id);

            Post? post = _postRepository.GetById(id);

            PostResponseDTO postResponseDTO = post;

            return new Response<PostResponseDTO>()
            {
                Data = postResponseDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<PostResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<PostResponseDTO> Update(PostUpdateRequest postUpdateRequest)
    {
        try
        {
            Post post = postUpdateRequest;

            _postRules.PostTitleMustBeValid(post.Title);
            _postRules.PostContentMustBeValid(post.Content);
            _postRepository.Update(post);

            PostResponseDTO postResponseDTO = post;

            return new Response<PostResponseDTO>()
            {
                Data = postResponseDTO,
                Message = "Gönderi başarıyla güncellendi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<PostResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
