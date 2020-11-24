// ReSharper disable VirtualMemberCallInConstructor
namespace FishMap.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FishMap.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.GroupTripsGuest = new HashSet<UserGroupTrip>();
            this.GroupTripsHost = new HashSet<GroupTrip>();
            this.Trips = new HashSet<Trip>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<UserGroupTrip> GroupTripsGuest { get; set; }

        public virtual ICollection<GroupTrip> GroupTripsHost { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
