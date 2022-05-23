using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.DBO
{
    public class CommentDbo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public uint Rating { get; set; }
        public string Text { get; set; }
        public Guid RecipeDboId { get; set; }
        public RecipeDbo RecipeDbo { get; set; }
    }
}
