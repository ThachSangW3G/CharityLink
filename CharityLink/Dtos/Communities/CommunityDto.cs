﻿using CharityLink.Dtos.Donations;
using CharityLink.Dtos.Posts;

using CharityLink.Models;

namespace CharityLink.Dtos.Communities
{
    public class CommunityDto
    {
        public int CommunityId { get; set; }

        public string CommunityName { get; set; }

        public string Description { get; set; }

        public PublishStatus PublishStatus { get; set; }

        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal? TargetAmount { get; set; }

        public string ImageUrl { get; set; }

        public decimal CurrentAmount { get; set; }
        public int DonationCount { get; set; }

        public string Type { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set;}



    }
}
