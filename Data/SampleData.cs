
//Uncomment to seed random data



using AutoFixture;
using WatchMe.Models;
namespace WatchMe.Data
{
    public static class Seeder
    {
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
                   new User { Username = "sami1234", Password="12345", IsAdmin=true,
                   MyBar = new Bar {
                       Name = "Zona", Location = "Zaragoza",Capacity = 50,
                        Shows = new Show[] {
                           new Show{Title= "Francias - España (Eurocopa)", Start = DateTime.Now, End= DateTime.Now.AddHours(1.3),Sport="Football"},
                           new Show{Title= "Francias - rwrrwrw (Eurocopa)", Start = DateTime.Now, End= DateTime.Now.AddHours(1.3),Sport="Football"},
                           }
                         }
                        },
                   new User {  Username = "VeggieXR2", Password="543533",  IsAdmin = false,
                   MyBar = new Bar {  Name = "Classic Italian", Location = "Zaragoza",Capacity = 50,
                   Shows = new Show[] {
                       new Show{Title= "Test",Start = DateTime.Now, End= DateTime.Now.AddHours(1),Sport = "Football"},
                       new Show{Title= "Test2",Start = DateTime.Now, End= DateTime.Now.AddHours(1),Sport="Volleyball"},
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
