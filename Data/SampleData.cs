// //Uncomment to seed random data



// using AutoFixture;
// using WatchMe.Models;

// namespace WatchMe.Data
// {
//     public static class Seeder
//     {
//         public static void Seed(this DataContext dbContext)
//         {
//             if (!dbContext.Users.Any())
//             {
//                 // Fixture fixture = new Fixture();
//                 // fixture.Customize<User>(User => User.Without(p => p.Id));
//                 // fixture.Customize<Bar>(Bar => Bar.Without(b => b.Id));
//                 // --- The next two lines add 100 rows to your database
//                 // List<User> Users = fixture.CreateMany<User>(10).ToList();
//                 // List<Bar> Bars = fixture.CreateMany<Bar>(10).ToList();
//                 List<User> Users = new List<User>
//                 {
//                     new User
//                     {
//                         Username = "sami1234",
//                         Password = "12345",
//                         IsAdmin = true,
//                         MyBars = new Bar[]
//                         {
//                             new Bar
//                             {
//                                 Name = "Zona",
//                                 Location = "Zaragoza",
//                                 Capacity = 50,
//                                 Description =
//                                     "Experience the ultimate sports atmosphere at The Stadium Bar, with multiple big screens, great food, and drinks that will keep you cheering all night long.",
//                                 Shows = new Show[]
//                                 {
//                                     new Show
//                                     {
//                                         Title = "Francias - España (Eurocopa)",
//                                         Start = DateTime.Now,
//                                         End = DateTime.Now.AddHours(1.3),
//                                         Sport = "Football"
//                                     },
//                                     new Show
//                                     {
//                                         Title = "Francias - rwrrwrw (Eurocopa)",
//                                         Start = DateTime.Now,
//                                         End = DateTime.Now.AddHours(1.3),
//                                         Sport = "Football"
//                                     },
//                                 }
//                             }
//                         }
//                     },
//                     new User
//                     {
//                         Username = "VeggieXR2",
//                         Password = "543533",
//                         IsAdmin = false,
//                         MyBars = new Bar[]
//                         {
//                             new Bar
//                             {
//                                 Name = "Classic Italian",
//                                 Location = "Zaragoza",
//                                 Description =
//                                     "Step into the world of craft beers and delicious food at The Brew House, where you'll find an extensive selection of local and international brews and a cozy atmosphere perfect for a night out.",
//                                 Capacity = 50,
//                                 Shows = new Show[]
//                                 {
//                                     new Show
//                                     {
//                                         Title = "Test",
//                                         Start = DateTime.Now,
//                                         End = DateTime.Now.AddHours(1),
//                                         Sport = "Rugby"
//                                     },
//                                     new Show
//                                     {
//                                         Title = "Test2",
//                                         Start = DateTime.Now,
//                                         End = DateTime.Now.AddHours(1),
//                                         Sport = "Volleyball"
//                                     },
//                                 }
//                             }
//                         }
//                     },
//                     new User
//                     {
//                         Username = "john_doe",
//                         Password = "p@ssw0rd",
//                         IsAdmin = false,
//                         MyBars = new Bar[]
//                         {
//                             new Bar
//                             {
//                                 Name = "The Sports Bar",
//                                 Location = "New York City",
//                                 Description =
//                                     "Experience the ultimate sports atmosphere at The Stadium Bar, with multiple big screens, great food, and drinks that will keep you cheering all night long.",
//                                 Capacity = 100,
//                                 Shows = new Show[]
//                                 {
//                                     new Show
//                                     {
//                                         Title = "NBA Finals",
//                                         Start = DateTime.Now,
//                                         End = DateTime.Now.AddHours(2),
//                                         Sport = "Basketball"
//                                     },
//                                     new Show
//                                     {
//                                         Title = "New York Yankees vs Boston Red Sox",
//                                         Start = DateTime.Now.AddHours(3),
//                                         End = DateTime.Now.AddHours(5),
//                                         Sport = "Baseball"
//                                     }
//                                 }
//                             }
//                         }
//                     },
//                     new User
//                     {
//                         Username = "jane_smith",
//                         Password = "p@ssw0rd123",
//                         IsAdmin = true,
//                         MyBar = new Bar
//                         {
//                             Name = "The Locker Room",
//                             Location = "Los Angeles",
//                             Capacity = 80,
//                             Description =
//                                 "Get a taste of the Caribbean at The Tiki Bar, where the drinks are tropical and the music is lively. Relax under the palm trees and indulge in delicious island-inspired cuisine while you soak up the sun.",
//                             Shows = new Show[]
//                             {
//                                 new Show
//                                 {
//                                     Title = "UFC Fight Night",
//                                     Start = DateTime.Now.AddDays(2),
//                                     End = DateTime.Now.AddDays(3),
//                                     Sport = "Mixed Martial Arts"
//                                 },
//                                 new Show
//                                 {
//                                     Title = "Los Angeles Lakers vs Golden State Warriors",
//                                     Start = DateTime.Now.AddHours(6),
//                                     End = DateTime.Now.AddHours(8),
//                                     Sport = "Basketball"
//                                 }
//                             }
//                         }
//                     }
//                 };
//                 dbContext.AddRange(Users);
//                 dbContext.SaveChanges();
//             }
//         }
//     }
// }
