using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;

namespace RecipeHub.Domain.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentInfrastructureService _commentInfrastructureService;
        private readonly IRecipeInfrastructureService _recipeInfrastructureService;

        public CommentService(ICommentInfrastructureService commentInfrastructureService, IRecipeInfrastructureService recipeInfrastructureService)
        {
            _commentInfrastructureService = commentInfrastructureService;
            _recipeInfrastructureService = recipeInfrastructureService;
        }

        public CommentService()
        {
        }

        public void CreateComment(Guid userId, Guid recipeId, Comment comment)
        {
            if (_commentInfrastructureService.GetCommentsByRecipeId(recipeId))
            {
                throw new CommentException($"User with Id {userId} already posted comment on recipe with Id {recipeId}. CreateComment failed");
            }
            _recipeInfrastructureService.GetById(recipeId).Comments.Add(comment);
        }

        public IEnumerable<Comment> ReadComments(Guid userId, Guid recipeId)
        {
            return _commentInfrastructureService.GetCommentsByRecipeId(recipeId);
        }

        public void UpdateComment(Guid userId, Guid recipeId, Comment comment)
        {
            if (_recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId) == null)
            {
                throw new CommentException($"Comment from user with Id {userId} does not exist on recipe with Id {recipeId}. UpdateComment failed.");
            }
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Id = comment.Id;
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Rating = comment.Rating;
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Text = comment.Text;
        }

        public void DeleteComment(Guid userId, Guid recipeId)
        {
            if (_recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId) == null)
            {
                throw new CommentException($"Comment from user with Id {userId} does not exist on recipe with Id {recipeId}. DeleteComment failed.");
            }
            var comment = _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId);
            _recipeInfrastructureService.GetById(recipeId).Comments.Remove(comment);
        }
    }
}
