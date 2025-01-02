using CharityLink.Models;

namespace CharityLink.Dtos.Communities
{
    public class UpdateCommunityRequestDto
    {
        public string CommunityName { get; set; }

        public string Description { get; set; }

        public PublishStatus PublishStatus { get; set; }

        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TargetAmount { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
