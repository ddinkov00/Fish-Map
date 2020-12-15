using FishMap.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishMap.Data.Models
{
    public class Comment : BaseDeletableModel<int>
    {
        public string Message { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
