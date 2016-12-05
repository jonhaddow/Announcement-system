namespace Coursework.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        //  This method will be called after migrating to the latest version.
        protected override void Seed(ApplicationDbContext context)
        {
            // Define information to seed
            string roleName = "lecturer";
            string lecturerUsername = "Lecturer1@email.com";
            string[] studentUsernames =
            {
                "Student1@email.com",
                "Student2@email.com",
                "Student3@email.com",
                "Student4@email.com",
                "Student5@email.com"
            };
            string defaultPassword = "password";

            // If role has not already been assigned
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                // Get role manager and create lecturer role
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var identityRole = roleManager.Create(new IdentityRole(roleName));
            }

            // Get user manager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // If lecturer data is not already registered
            if (!context.Users.Any(u => u.UserName == lecturerUsername))
            {
                // Seed lecturer data and role
                ApplicationUser lecturer = new ApplicationUser { UserName = lecturerUsername };
                userManager.Create(lecturer, defaultPassword);
                userManager.AddToRole(lecturer.Id, roleName);
            }

            // Seed student data
            foreach (string username in studentUsernames)
            {
                ApplicationUser student = new ApplicationUser { UserName = username };
                userManager.Create(student, defaultPassword);
            }
        }
    }
}
