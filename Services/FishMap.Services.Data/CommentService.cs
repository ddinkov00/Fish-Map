﻿namespace FishMap.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Services.Data.Contracts;
    using FishMap.Web.ViewModels.Comments;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CreateForGroupTrip(int groupTripId, string content, string userId)
        {
            var comment = new Comment
            {
                Content = content,
                GroupTripId = groupTripId,
                UserId = userId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task CreateForTrip(int tripId, string content, string userId)
        {
            var comment = new Comment
            {
                Content = content,
                TripId = tripId,
                UserId = userId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<CommentViewModel> GetComentsByTripId(int tripId)
        {
            var comments = this.commentRepository.All()
                .Where(c => c.TripId == tripId)
                .OrderBy(c => c.CreatedOn)
                .Select(c => new CommentViewModel
                {
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    AddedByUserEmail = c.User.Email,
                });

            return comments;
        }
    }
}
