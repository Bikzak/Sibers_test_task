using DomainModels;
using DomainModels.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;

namespace DataAccessLayer.EF
{
    public class ApplicationUser : IdentityUser
    {
        public async System.Threading.Tasks.Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ConnectionToDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Projects> Project { get; set; }
        public DbSet<Workers> Worker { get; set; }
        public DbSet<WorkerProjects> WorkerProjects { get; set; }
        public DbSet<Tasks> Task { get; set; }
    }
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{

    //    public ApplicationDbContext()
    //        : base("ConnectionToDb")
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //    public DbSet<Project> Projects { get; set; }
    //    public DbSet<Worker> Workers { get; set; }
    //    public DbSet<WorkerProjects> WorkerProjects { get; set; }
    //    public DbSet<Task> Tasks { get; set; }
    //}

}
