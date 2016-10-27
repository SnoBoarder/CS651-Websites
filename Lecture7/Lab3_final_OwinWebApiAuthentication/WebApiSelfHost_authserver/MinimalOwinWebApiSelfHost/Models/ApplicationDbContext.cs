using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add using:
using System.Data.Entity;
using System.Security.Claims;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("MyDatabase")
        {

        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public IDbSet<Company> Companies { get; set; }
        public IDbSet<MyUser> Users { get; set; }
        public IDbSet<MyUserClaim> Claims { get; set; }
    }


    public class ApplicationDbInitializer 
        : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected async override void Seed(ApplicationDbContext context)
        {
            context.Companies.Add(new Company { Name = "Microsoft" });
            context.Companies.Add(new Company { Name = "Apple" });
            context.Companies.Add(new Company { Name = "Google" });
            context.SaveChanges();

            // Set up two initial users with different role claims:
            var dino = new MyUser { Email = "dinok@bu.edu" };
            var dino2 = new MyUser { Email = "dino@mitre.org" };

            dino.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = dino.Id, ClaimValue = dino.Email });
            dino.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = dino.Id, ClaimValue = "Admin" });

            dino2.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = dino2.Id, ClaimValue = dino2.Email });
            dino2.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = dino2.Id, ClaimValue = "User" });

            var store = new MyUserStore(context);
            await store.AddUserAsync(dino, "DinosPassword");
            await store.AddUserAsync(dino2, "Dino2sPassword");
        }
    }
}
