using System;
using CreateMultipleUsersConsoleApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CreateMultipleUsersConsoleApp
{
    public class PopulateUsers
    {
        public static void CreateUser(ApplicationUser user, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            IdentityResult response = userManager.Create(user, password);
            if (response.Succeeded)
            {

                Console.WriteLine("User \"{0} {1}\" has been created.", user.FirstName, user.LastName);
            }
            else
            {
                var errors = response.Errors;
                foreach (var error in errors)
                {
                    Console.WriteLine("User creation \"{0} {1}\" failed.\nERROR: {2}", user.FirstName, user.LastName, error);
                }
            }
        }
    }
}
