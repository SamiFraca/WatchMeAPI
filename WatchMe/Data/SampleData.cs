
//Uncomment to seed random data



using AutoFixture;
using WatchMe.Models;
namespace WatchMe.Data
{
    public static class Seeder
    {
        static int generateId(int id)
        {
            return id++;
        }
        public static void Seed(this DataContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                // Fixture fixture = new Fixture();
                // fixture.Customize<User>(User => User.Without(p => p.Id));
                // fixture.Customize<Bar>(Bar => Bar.Without(b => b.Id));
                // --- The next two lines add 100 rows to your database
                // List<User> Users = fixture.CreateMany<User>(10).ToList();
                // List<Bar> Bars = fixture.CreateMany<Bar>(10).ToList();
                List<User> Users = new List<User>
               {
                   new User { Username = "Classic Italian", Password="12345", IsAdmin=true,
                   MyBar = new Bar {
                       Name = "Classic sddddd", Location = "Zaragoza",Capacity = 50,
                        Show = new Show {
                           Title= "Francia - España (Eurocopa)", Start = new DateTime(2108, 3, 1, 7, 0, 0, DateTimeKind.Utc), End= new DateTime(2028, 3, 1, 7, 0, 0, DateTimeKind.Utc)
                           }
                         }
                        },
                   new User {  Username = "Veggie", Password="543533",  IsAdmin = false,
                   MyBar = new Bar {  Name = "Classic Italian", Location = "Zaragoza",Capacity = 50,
                   Show = new Show {
                       Title= "Test",Start = DateTime.Now, End= new DateTime(2010, 3, 1, 7, 0, 0, DateTimeKind.Utc)
                       }
                     }
                    }
               };
                dbContext.AddRange(Users);
                dbContext.SaveChanges();
            }
        }
    }
}
