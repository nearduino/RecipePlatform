using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Implementations
{
    public class CommentLogic : ICommentLogic
    {
        private readonly Dictionary<Tuple<int, int>, Comment> Comments = new Dictionary<Tuple<int, int>, Comment>();

        public void CreateComment(int userId, int recipeId, Comment comment)
        {
            // ovo ce morati drugacije
            // komentar treba da bude vezan za odredjeni recept, ali takodje i za odredjenog korisnika
            // treba iskombinovati ta dva id-a u jedan nekako
            Comments.Add(new Tuple<int, int>(userId, recipeId), comment);
        }

        public Comment ReadComment(int userId, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(int userId, int recipeId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int userId, int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
