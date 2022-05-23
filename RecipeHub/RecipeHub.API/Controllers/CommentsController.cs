using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Attributes;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_commentService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_commentService.GetById(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Comment not found");
            }
            catch (Exception)
            {
                return Problem("Oops, something went wrong. Try again later!");
            }
        }

        [JwtUserAuthorization]
        [HttpPost]
        public IActionResult PostComment(CommentDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            var comment = _mapper.Map<Comment>(dto);
            comment.UserId = userId;
            try
            {
                _commentService.CreateComment(comment, dto.RecipeId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Problem("Oops, something went wrong. Try again later!");
            }

            return Ok("Comment posted successfully");
        }

        [JwtUserAuthorization]
        [HttpPut]
        public IActionResult UpdateComment(UpdateCommentDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            try
            {
                if (userId != _commentService.GetById(dto.Id).UserId)
                {
                    return Unauthorized();
                }
                var comment = _mapper.Map<Comment>(dto);
                comment.UserId = userId;
                _commentService.UpdateComment(comment);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Comment not found");
            }
            catch (Exception)
            {
                return Problem("Oops, something went wrong. Try again later!");
            }
            return Ok("Comment updated successfully!");
        }

        [JwtAdminAuthorization]
        [HttpDelete]
        public IActionResult DeleteComment(DeleteCommentDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            bool isAdmin = HttpContext.Items["isAdmin"].Equals("True");
            try
            {
                var comment = _commentService.GetById(dto.Id);
                if (!isAdmin && userId != comment.UserId) return Unauthorized();
                _commentService.DeleteComment(dto.Id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Comment not found!");
            }
            catch (Exception)
            {
                return Problem("Oops, something went wrong. Try again later!");
            }
            return Ok("Comment deleted");
        }
    }
}
