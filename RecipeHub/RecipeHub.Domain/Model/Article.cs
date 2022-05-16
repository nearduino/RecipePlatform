using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Article()
        {

        }

        public Article(int id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

    }
}
