using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;
using RecipeHub.Domain.Utilities;

namespace RecipeHub.Domain.Model
{
    public class Recipe
    {
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }        
        public string Instructions { get; private set; }

        private readonly List<Comment> _comments;
        public List<Comment> Comments => new List<Comment>(_comments);

        private readonly List<RecipeIngredient> _recipeIngredients;
        public List<RecipeIngredient> RecipeIngredients => new List<RecipeIngredient>(_recipeIngredients);
        public uint PreparationTime { get; set; }
        public string ImgSrc { get; private set; }

        public Recipe(Category category, string name, string desc, string instructions, uint preparationTime,
            List<RecipeIngredient> recipeIngredients, List<Comment> comments)
        {
            Category = category;
            Name = name;
            Description = desc;
            Instructions = instructions;
            PreparationTime = preparationTime;
            _recipeIngredients = new List<RecipeIngredient>();
            _comments = comments;
            Validate();
        }

        private void Validate()
        {
            if (!TextValidator.CheckName(Name)) throw new InvalidNameException();
            if (Description.Length == 0) throw new ArgumentException("Description must not be empty");
            if (Instructions.Length == 0) throw new ArgumentException("Instructions must not be empty");
        }

        public string GetImage()
        {
            //Vratiti sliku u Base64 stringu?
            return null;
        }

        public void AddImageSource(string imgSrc)
        {
            ImgSrc = imgSrc;
        }

        public void AddIngredient(RecipeIngredient recipeIngredient)
        {
            _recipeIngredients.Add(recipeIngredient);
        }

        public double CalculateRecipeRating()
        {
            double rating = 0;
            foreach(Comment comment in _comments) rating += comment.Rating;
            return rating / _comments.Count;
        }
    }
}
