namespace CharityLink.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt {  get; set; }
        public bool IsRead { get; set; }
        public String? ImageUrl {  get; set; }


        public void SetIsRead()
        {
            IsRead = true;
        }
    }
}
