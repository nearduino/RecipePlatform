using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.Domain.Model
{
    public class Comment
    {
        public int Id { get; private set; }
        public uint Rating { get; private set; }
        public string Text { get; private set; }

        public Comment(uint rating, string text)
        {
            Rating = rating;
            Text = text;
            Validate();
        }

        public Comment(int id, uint rating, string text)
        {
            Rating = rating;
            Text = text;
            Id = id;
            Validate();
        }

        private void Validate()
        {
            if (Rating is < 1 or > 10) throw new InvalidRatingException();
            if (Text.Length == 0) throw new ArgumentException("Text must not be empty");
        }
    }
}
