using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string City { get; set; }
        public bool IsPrivate { get; set; }
        public virtual Cv Cv { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UsersInProject> UsersInProjects { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public DbSet<Cv> GetCvs { get; set; }
        public DbSet<Message> GetMessages { get; set; }
        public DbSet<MessageInfo> GetMessageInfos { get; set; }
        public DbSet<Project> GetProjects { get; set; }
        public DbSet<UsersInProject> GetUsersInProjects { get; set; }
        public object Search { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().
                   HasRequired<ApplicationUser>(x => x.Creator)
                   .WithMany(x => x.Projects)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>().HasMany(x => x.UserProject)
                    .WithRequired(x => x.Project);


            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.UsersInProjects)
                    .WithRequired(x => x.User);

            modelBuilder.Entity<UsersInProject>()
                     .HasKey(x => new { x.ProjectId, x.ApplicationUserID });

            modelBuilder.Entity<MessageInfo>()
                    .HasKey(x => new { x.ReceiverId, x.MessageId });

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId)
                .ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id)
                .ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId })
                .ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("IdentityUserClaim");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Cv>().ToTable("Cv");

        }
            public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
