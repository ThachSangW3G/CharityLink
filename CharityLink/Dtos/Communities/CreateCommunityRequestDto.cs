﻿using CharityLink.Dtos.Donations;
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
       
    }
}
