using RecipeHub.Domain.Abstractions;
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


        public IEnumerable<Comment> GetAll(Guid recipeId)
        {
            return _recipeInfrastructureService.GetById(recipeId).Comments;
        }

        public void CreateComment(Guid userId, Guid recipeId, Comment comment)
        {
            _recipeInfrastructureService.GetById(recipeId).Comments.Add(comment);
        }

        public Comment ReadComment(Guid userId, Guid recipeId)
        {
            var comments = _recipeInfrastructureService.GetById(recipeId).Comments;
            return comments.Find(c => c.Id == userId);
        }

        public void UpdateComment(Guid userId, Guid recipeId, Comment comment)
        {
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Id = comment.Id;
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Rating = comment.Rating;
            _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId).Text = comment.Text;
        }

        public void DeleteComment(Guid userId, Guid recipeId)
        {
            var comment = _recipeInfrastructureService.GetById(recipeId).Comments.Find(c => c.Id == userId);
            _recipeInfrastructureService.GetById(recipeId).Comments.Remove(comment);
        }
    }
}
