using System;

namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? User { get; set; } // Adicionado '?'
        public string? Text { get; set; } // Adicionado '?'
        public DateTime Timestamp { get; set; }
    }
}