using System;

namespace Aggregator.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public uint Rating { get; set; }
        public string Text { get; set; }
    }
}
