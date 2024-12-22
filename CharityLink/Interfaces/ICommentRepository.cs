using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int Id);
        Task<Comment> CreateAsync(Comment Comment);
        Task<Comment?> UpdateAsync(int Id, Comment Comment);
        Task<Comment?> DeleteAsync(int Id);
        Task<List<Comment>> GetCommentsByPostId(int PostId);
        Task<List<Comment>> GetParentCommentByPostId (int PostId);
        Task<List<Comment>> GetChildrenCommentByParentId(int ParentCommentId);
        Task<int> CountComment(int PostId);
       
    }
}
