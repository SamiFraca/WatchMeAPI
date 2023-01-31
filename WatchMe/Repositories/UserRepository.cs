// using EFCoreInMemoryDbDemo.Models;
// using Microsoft.EntityFrameworkCore;
// namespace EFCoreInMemoryDbDemo
// {
//     interface IUserRepository
//     {
//         public List<User> GetUsers();
//     }
//     public class UserRepository : IUserRepository
//     {
//         public UserRepository()
//         {
//             using (var context = new DataContext())
//             {
//                 Users = new List<User>
//         {
//             new User { Id = 1, Username = "Classic Italian", Password="12345", IsAdmin=true,
//             MyBar = new Bar {
//                 Id = 1, Name = "Classic sddddd", Location = "Zaragoza",Capacity = 50,
//                  Show = new Show {
//                     Title= "Francia - España (Eurocopa)", Start = new DateTime(2108, 3, 1, 7, 0, 0, DateTimeKind.Utc), End= new DateTime(2028, 3, 1, 7, 0, 0, DateTimeKind.Utc)
//                     }
//                   }
//                  },
//             new User { Id = 2, Username = "Veggie", Password="543533",  IsAdmin = false,
//             MyBar = new Bar {  Id = 2, Name = "Classic Italian", Location = "Zaragoza",Capacity = 50,
//             Show = new Show {
//                 Title= "Test",Start = DateTime.Now, End= new DateTime(2010, 3, 1, 7, 0, 0, DateTimeKind.Utc)
//                 }
//               }
//              }
//         };
//             };
//             context.Users.AddRange(Users);
//             context.SaveChanges();
//         }

//         public List<User> GetUsers()
//         {
//             using (var context = new DataContext())
//             {
//                 var list = context.Users
//                     .Include(a => a.Books)
//                     .ToList();
//                 return list;
//             }
//         }
//     }
// }