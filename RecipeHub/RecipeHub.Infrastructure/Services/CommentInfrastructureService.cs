using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.Infrastructure.DBO;
using Mapper = RecipeHub.Infrastructure.Mappers.Mapper;

namespace RecipeHub.Infrastructure.Services
{
    public class CommentInfrastructureService : ICommentInfrastructureService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentInfrastructureService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public IEnumerable<Comment> GetAll()
        {
            List<Comment> comments = new List<Comment>();
            var commentDbos = _commentRepository.GetAll().ToList();
            foreach (var commentDbo in commentDbos)
            {
                comments.Add(_mapper.Map<Comment>(commentDbo));
            }
            return comments;
        }

        public void Update(Comment comment)
        {
            var fromDatabase = _commentRepository.getById(comment.Id);
            var dbo = _mapper.Map<CommentDbo>(comment);
            _mapper.Map(dbo, fromDatabase);
            fromDatabase.RecipeDbo = null;
            _commentRepository.Update(fromDatabase);
        }

        public void Delete(Guid commentId)
        {
            _commentRepository.Delete(_commentRepository.getById(commentId));
        }

        public Comment GetById(Guid commentId)
        {
            return _mapper.Map<Comment>(_commentRepository.getById(commentId));
        }

        public void CreateComment(Comment comment, Guid recipeId)
        {
            var dbo = _mapper.Map<CommentDbo>(comment);
            dbo.RecipeDboId = recipeId;
            _commentRepository.Add(dbo);
        }

        public IEnumerable<Comment> GetCommentsByIds(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByRecipeId(Guid recipeID)
        {
            throw new NotImplementedException();
        }
    }
}
