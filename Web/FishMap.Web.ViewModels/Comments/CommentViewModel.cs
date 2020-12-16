namespace FishMap.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommentViewModel
    {
        public string Content { get; set; }

        public string AddedByUserEmail { get; set; }

        public string AddedByUsername => this.AddedByUserEmail.Split('@').ElementAt(0);

        public DateTime CreatedOn { get; set; }

        public string CreatedOnString => this.CreatedOn.ToString("dd/MM/yyyy hh:mm tt");

        public IEnumerable<CommentViewModel> Replies { get; set; }
    }
}
