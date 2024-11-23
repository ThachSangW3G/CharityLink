namespace CharityLink.Dtos.Communities
{
    public class UpdateCommunityRequestDto
    {
        public string CommunityName { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
