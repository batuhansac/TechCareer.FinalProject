using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Service.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.Concretes;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentRules _commentRules;

    public CommentService(ICommentRepository commentRepository, ICommentRules commentRules)
    {
        _commentRepository = commentRepository;
        _commentRules = commentRules;
    }
    public Response<CommentResponseDTO> Add(CommentAddRequest commentAddRequest)
    {
        try
        {
            Comment comment = commentAddRequest;

            _commentRules.CommentContentMustBeValid(comment.Content);

            _commentRepository.Add(comment);

            CommentResponseDTO commentResponseDTO = comment;

            return new Response<CommentResponseDTO>()
            {
                Data = commentResponseDTO,
                Message = "Yorum başarıyla oluşturuldu.",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CommentResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CommentResponseDTO> Delete(int id)
    {
        try
        {
            _commentRules.CommentIsPresent(id);

            Comment comment = _commentRepository.GetById(id);

            _commentRepository.Delete(comment);

            CommentResponseDTO commentResponseDTO = comment;

            return new Response<CommentResponseDTO>()
            {
                Data = commentResponseDTO,
                Message = "Yorum başarıyla silindi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CommentResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<CommentResponseDTO>> GetAll()
    {
        List<Comment> comments = _commentRepository.GetAll();

        List<CommentResponseDTO> commentDetailDTOs = comments.Select(c => (CommentResponseDTO)c).ToList();

        return new Response<List<CommentResponseDTO>>()
        {
            Data = commentDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<CommentResponseDTO>> GetAllByDatePosted(short dateBegin, short dateEnd)
    {
        List<Comment> comments = _commentRepository.GetAll(c => c.DatePosted <= dateBegin && c.DatePosted >= dateEnd);

        List<CommentResponseDTO> commentDetailDTOs = comments.Select(c => (CommentResponseDTO)c).ToList();

        return new Response<List<CommentResponseDTO>>()
        {
            Data = commentDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<CommentDetailDTO>> GetAllDetails()
    {
        List<CommentDetailDTO> commentDetailDTOs = _commentRepository.GetAllCommentDetails();

        return new Response<List<CommentDetailDTO>>()
        {
            Data = commentDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<CommentDetailDTO>> GetAllDetailsByPostId(int postId)
    {
        List<CommentDetailDTO> commentDetailDTOs = _commentRepository.GetDetailsByPostId(postId);

        return new Response<List<CommentDetailDTO>>()
        {
            Data = commentDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<CommentDetailDTO>> GetAllDetailsByUserId(Guid userId)
    {
        List<CommentDetailDTO> commentDetailDTOs = _commentRepository.GetDetailsByUserId(userId);

        return new Response<List<CommentDetailDTO>>()
        {
            Data = commentDetailDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<CommentDetailDTO> GetByDetailId(int id)
    {
        try
        {
            _commentRules.CommentIsPresent(id);

            CommentDetailDTO commentDetailDTO = _commentRepository.GetCommentDetail(id);

            return new Response<CommentDetailDTO>()
            {
                Data = commentDetailDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CommentDetailDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CommentResponseDTO> GetById(int id)
    {
        try
        {
            _commentRules.CommentIsPresent(id);

            Comment? comment = _commentRepository.GetById(id);

            CommentResponseDTO commentResponseDTO = comment;

            return new Response<CommentResponseDTO>()
            {
                Data = commentResponseDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CommentResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CommentResponseDTO> Update(CommentUpdateRequest commentUpdateRequest)
    {
        try
        {
            Comment comment = commentUpdateRequest;

            _commentRules.CommentContentMustBeValid(comment.Content);
            _commentRepository.Update(comment);

            CommentResponseDTO commentResponseDTO = comment;

            return new Response<CommentResponseDTO>()
            {
                Data = commentResponseDTO,
                Message = "Yorum başarıyla güncellendi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };

        }
        catch (BusinessException ex)
        {
            return new Response<CommentResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
