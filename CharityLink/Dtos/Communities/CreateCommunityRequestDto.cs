using CharityLink.Dtos.Donations;
using CharityLink.Dtos.Posts;

namespace CharityLink.Dtos.Communities
{
    public class CreateCommunityRequestDto
    {

        public string CommunityName { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TargetAmount { get; set; }
        public string Type { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set;}

    }
}
