namespace SoccerStats.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SoccerStats.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SoccerStats.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SoccerStats.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Add a user to the db
            context.Users.AddOrUpdate(
                u => u.UserName,
                new Models.ApplicationUser
                {
                    UserName = "hectuanerz@gmail.com",
                    Email = "hectuanerz@gmail.com",
                }
                );

            context.SaveChanges();
            // Create a UserManager to add a password to the previously created user
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            UserManager.AddPassword(context.Users.Where(u => u.UserName == "hectuanerz@gmail.com").FirstOrDefault().Id.ToString(), "123456");
            context.SaveChanges();
        }
    }
}
