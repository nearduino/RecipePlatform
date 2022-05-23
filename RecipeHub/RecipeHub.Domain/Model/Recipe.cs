using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;
using RecipeHub.Domain.Utilities;

namespace RecipeHub.Domain.Model
{
    public class Recipe
    {
        public Guid Id { get; private set; }
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }        
        public string Instructions { get; private set; }

        private List<Comment> _comments;
        //public List<Comment> Comments => new (_comments);
        public List<Comment> Comments
        {
            get => new(_comments);
            private set => _comments = value;
        }

        private List<RecipeIngredient> _recipeIngredients;
        //public List<RecipeIngredient> RecipeIngredients => new (_recipeIngredients);
        public List<RecipeIngredient> RecipeIngredients
        {
            get => new (_recipeIngredients);
            private set => _recipeIngredients = value;
        }
        public uint PreparationTime { get; set; }
        public string ImgSrc { get; private set; }

        public Guid UserId { get; private set; }

        private Recipe()
        {
            _comments = new List<Comment>();
            _recipeIngredients = new List<RecipeIngredient>();
        }

        public Recipe(Category category, string name, string desc, string instructions, uint preparationTime,
            List<RecipeIngredient> recipeIngredients, List<Comment> comments, Guid userId, Guid id)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = desc;
            Instructions = instructions;
            PreparationTime = preparationTime;
            _recipeIngredients = recipeIngredients;
            _comments = comments;
            Validate();
        }

        public Recipe(Category category, string name, string desc, string instructions, uint preparationTime,
            List<RecipeIngredient> recipeIngredients, Guid userId, Guid id)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = desc;
            Instructions = instructions;
            PreparationTime = preparationTime;
            _recipeIngredients = recipeIngredients;
            _comments = new List<Comment>();
            Validate();
        }

        public Recipe(Category category, string name, string desc, string instructions, uint preparationTime,
            List<RecipeIngredient> recipeIngredients, List<Comment> comments, Guid userId)
        {
            UserId = userId;
            Category = category;
            Name = name;
            Description = desc;
            Instructions = instructions;
            PreparationTime = preparationTime;
            _recipeIngredients = recipeIngredients;
            _comments = comments;
            Validate();
        }

        public Recipe(Category category, string name, string desc, string instructions, uint preparationTime,
            List<RecipeIngredient> recipeIngredients, Guid userId)
        {
            Category = category;
            Name = name;
            Description = desc;
            Instructions = instructions;
            PreparationTime = preparationTime;
            _recipeIngredients = recipeIngredients;
            _comments = new List<Comment>();
            UserId = userId;
            Validate();
        }

        private void Validate()
        {
            if (!TextValidator.CheckName(Name)) throw new InvalidNameException();
            if (Description.Length == 0) throw new InvalidDescriptionException();
            if (Instructions.Length == 0) throw new InvalidRecipeInstructionsException();
            if (_recipeIngredients.Count == 0) throw new EmptyRecipeIngredientsException();
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
            if (_comments.Count == 0) throw new RecipeRatingCalculationException();
            double rating = 0;
            foreach (var comment in _comments) rating += comment.Rating;
            return rating / _comments.Count;
        }

        public void UpdateDescription(string desc)
        {
            Description = desc;
            Validate();
        }

        public void UpdateInstructions(string instr)
        {
            Instructions = instr;
            Validate();
        }

        public void UpdateIngredients(List<RecipeIngredient> ingredients)
        {
            _recipeIngredients = ingredients;
            Validate();
        }

        public void UpdatePreparationTime(uint prepTime)
        {
            PreparationTime = prepTime;
            Validate();
        }

        public void UpdateName(string name)
        {
            Name = name;
            Validate();
        }

        public void UpdateCategory(Category category)
        {
            Category = category;
            Validate();
        }

        public bool UserCommented(Guid userId)
        {
            foreach (var comment in _comments)
            {
                if (comment.UserId == userId) return true;
            }

            return false;
        }
    }
}
