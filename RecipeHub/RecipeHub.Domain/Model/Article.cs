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
        public string Title { get; set; }
        public string Text { get; set; }

        public Article()
        {

        }

        public Article(Guid id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

    }
}
