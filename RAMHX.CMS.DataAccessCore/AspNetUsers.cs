using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        [NotMapped]
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        [NotMapped]
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        [NotMapped]
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    }
}
