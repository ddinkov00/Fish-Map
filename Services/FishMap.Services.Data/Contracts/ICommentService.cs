﻿namespace FishMap.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishMap.Web.ViewModels.Comments;

    public interface ICommentService
    {
        Task Create(int tripId, string content, string userId);

        IEnumerable<CommentViewModel> GetComentsByTripId(int tripId);

        IEnumerable<CommentViewModel> GetRepliesByParentId(int parentId);
    }
}
