using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model
{
    public class Article
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Article()
        {

        }

        public Article(Guid id, Guid userId, string title, string text)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Text = text;
        }

    }
}
