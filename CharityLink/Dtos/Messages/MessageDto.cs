namespace CharityLink.Dtos.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        public int UnreadCount { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string? ImageUrl { get; set; }
    }
}
