using FishMap.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishMap.Data.Models
{
    public class Town : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
