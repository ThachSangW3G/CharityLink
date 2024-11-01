﻿using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _dBContext;

        public CommentRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment Comment)
        {
            await _dBContext.Comments.AddAsync(Comment);
            await _dBContext.SaveChangesAsync();
            return Comment;
        }

        public async Task<Comment?> DeleteAsync(int Id)
        {

            var comment = await _dBContext.Comments.FirstOrDefaultAsync(c => c.CommentId == Id);
            if (comment == null)
            {
                return null;
            }

            _dBContext.Comments.Remove(comment);
            await _dBContext.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _dBContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int Id)
        {
            return await _dBContext.Comments.FirstOrDefaultAsync(c => c.CommentId == Id);
        }

        public async Task<Comment?> UpdateAsync(int Id, Comment Comment)
        {
            var existingComment = await _dBContext.Comments.FindAsync(Id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Content = Comment.Content;
            existingComment.PostId = Comment.PostId;
            existingComment.UserId = Comment.UserId;
            existingComment.CreateDate = Comment.CreateDate;

            await _dBContext.SaveChangesAsync();
            return existingComment;
        }
    }
}
