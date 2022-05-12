using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.DBO
{
    public class CommentDbo
    {
        public int Id { get; set; }
        public uint Rating { get; set; }
        public string Text { get; set; }
    }
}
