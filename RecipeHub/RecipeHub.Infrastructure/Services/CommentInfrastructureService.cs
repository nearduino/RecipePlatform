using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.Mappers;
using RecipeHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.Services
{
    public class CommentInfrastructureService : ICommentInfrastructureService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentInfrastructureService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IEnumerable<Comment> GetAll()
        {
            List<Comment> comments = new List<Comment>();
            var commentDbos = _commentRepository.GetAll();
            foreach (var commentDbo in commentDbos)
            {
                comments.Add(Mapper.Map(commentDbo));
            }
            return comments;
        }

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }


        public Comment ReadComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCommnet(Comment commnet)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommnet(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
