namespace FirendOrganizerN2.DataAcces.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirendOrganizerN2.DataAcces.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirendOrganizerN2.DataAcces.FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Friends.AddOrUpdate(
                f => f.FirstName,
               new Friend { FirstName = "Rafael", LastName = "Grigoryan", Email = "--" },
               new Friend { FirstName = "Karen", LastName = "Barseghyan", Email = "--" },
               new Friend { FirstName = "Stive", LastName = "Voznyak", Email = "--" },
               new Friend { FirstName = "Gelelo", LastName = "Galelei", Email = "--" }
             );
        }
    }
}
