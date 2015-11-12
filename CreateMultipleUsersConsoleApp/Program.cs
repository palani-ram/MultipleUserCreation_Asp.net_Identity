using System;
using System.Data.Entity;
using System.IO;
using CreateMultipleUsersConsoleApp.Models;

namespace CreateMultipleUsersConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {

            // Recreate the database every time before popupate the data.
            // comment the below line to create a new db if the db is not exist
            Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());

            // uncomment the below line, if the db already exist.
            // Initialize the db context.
            //Database.SetInitializer<ApplicationDbContext>(null);

            Console.WriteLine("Path to pipe-delimited text file containing users to create: ");
            string textPath = Console.ReadLine();

            if (!string.IsNullOrEmpty(textPath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(textPath);
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrEmpty(line) && line.Trim().Substring(0, 1) != "#")
                        {

                            var userRecordInfo = line.Split(Convert.ToChar("|"));

                            var user = new ApplicationUser
                            {
                                UserName = userRecordInfo[0], // [0] UserName
                                FirstName = userRecordInfo[2], // [2] FirstName
                                LastName = userRecordInfo[3], // [3] LastName
                                IsConfirmed = true,
                                EmailConfirmed = true,
                                Email = userRecordInfo[4],
                                ConfirmationToken = Guid.NewGuid().ToString(),
                            };
                            PopulateUsers.CreateUser(user, userRecordInfo[1]); // [1] Password
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid path.");
                }
            }
            else
            {
                Console.WriteLine("Enter file path");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
