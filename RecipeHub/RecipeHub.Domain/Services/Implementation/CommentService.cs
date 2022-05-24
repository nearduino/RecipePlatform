using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using RecipeHub.Domain.Model.Exceptions;

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

        public void CreateComment(Comment comment, Guid recipeId)
        {
            Recipe recipe;
            try
            {
                recipe = _recipeInfrastructureService.GetById(recipeId);
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException("Recipe");
            }

            if (recipe.UserCommented(comment.UserId)) throw new UserAlreadyCommentedException();
            _commentInfrastructureService.CreateComment(comment, recipeId);
        }

        public IEnumerable<Comment> ReadComments(Guid userId, Guid recipeId)
        {
            return _commentInfrastructureService.GetCommentsByRecipeId(recipeId);
        }

        public void UpdateComment(Comment comment)
        {
            var fromDatabase = _commentInfrastructureService.GetById(comment.Id);
            fromDatabase.Rating = comment.Rating;
            fromDatabase.Text = comment.Text;
            _commentInfrastructureService.Update(comment);
        }

        public void DeleteComment(Guid commentId)
        {
            _commentInfrastructureService.Delete(commentId);
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

        public IEnumerable GetAll()
        {
            return _commentInfrastructureService.GetAll().ToList();
        }

        public Comment GetById(Guid commentId)
        {
            try
            {
                return _commentInfrastructureService.GetById(commentId);
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException("Comment");
            }
        }
    }
}
