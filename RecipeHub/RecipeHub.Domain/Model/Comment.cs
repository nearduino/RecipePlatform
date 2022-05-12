﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.Domain.Model
{
    public class Comment
    {
        public uint Rating { get; set; }
        public string Text { get; set; }

        public Comment(uint rating, string text)
        {
            Rating = rating;
            Text = text;
            Validate();
        }

        private void Validate()
        {
            if (Rating is < 1 or > 10) throw new InvalidRatingException();
            if (Text.Length == 0) throw new ArgumentException("Text must not be empty");
        }
    }
}