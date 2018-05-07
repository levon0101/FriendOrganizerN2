namespace FirendOrganizerN2.DataAcces.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirendOrganizerN2.DataAcces.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirendOrganizerN2.DataAcces.FriendOrganizerDbContext context)
        {

            context.Friends.AddOrUpdate(
                f => f.FirstName,
               new Friend { FirstName = "Rafael", LastName = "Grigoryan", }, // if error on seed() comment herar
               new Friend { FirstName = "Karen", LastName = "Barseghyan", },// if error on seed() comment herar
               new Friend { FirstName = "Stive", LastName = "Voznyak" },
               new Friend { FirstName = "Gelelo", LastName = "Galelei" }
               );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                    new ProgrammingLanguage { Name = "C#" },
                    new ProgrammingLanguage { Name = "JavaScript" },
                    new ProgrammingLanguage { Name = "TypeScript" },
                    new ProgrammingLanguage { Name = "C++" },
                    new ProgrammingLanguage { Name = "Python" }
                    );
            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(
              pl => pl.Number,
              new FriendPhoneNumber { Number = "+374 55 979 969", FriendId = context.Friends.First().Id }
              );

        }
    }
}
