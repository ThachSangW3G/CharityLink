namespace CharityLink.Dtos.Comments
{
    public class CommentWithChildrenDto
    {
        public CommentDto ParentComment { get; set; }
        public List<CommentDto> ChildComments { get; set; }
    }
}
