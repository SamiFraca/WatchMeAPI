using WatchMe.Data;
using WatchMe.Models;

namespace WatchMe.SampleData
{
    public class SampleData : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            new List<User>
            {
             new User { Id = 1, Username = "Classic Italian", Password="12345", IsAdmin=true,
            MyBar = new Bar {
                Id = 1, Name = "Classic sddddd", Location = "Zaragoza",Capacity = 50,
                 Show = new Show {
                    Title= "Francia - España (Eurocopa)", Start = new DateTime(2108, 3, 1, 7, 0, 0, DateTimeKind.Utc), End= new DateTime(2028, 3, 1, 7, 0, 0, DateTimeKind.Utc)
                    }
                  }
                 },
            new User { Id = 1, Username = "Veggie", Password="543533",  IsAdmin = false,
            MyBar = new Bar {  Id = 2, Name = "Classic Italian", Location = "Zaragoza",Capacity = 50,
            Show = new Show {
                Title= "Test",Start = DateTime.Now, End= new DateTime(2010, 3, 1, 7, 0, 0, DateTimeKind.Utc)
                }
              }
             }
            }.ForEach(i => context.Users.Add(i));

            new List<Bar>
            {
                new Bar { Id = 1, Name = "Classic Italian", Location = "Zaragoza",Capacity = 50, Show = new Show { Title= "Francia - España (Eurocopa)", Start = DateTime.Now, End= new DateTime(2010, 3, 1, 7, 0, 0, DateTimeKind.Utc)} },
            new Bar { Id = 2, Name = "Veggie", Location = "Pamplona",Capacity = 50, Show = new Show { Title= "Test", Start = DateTime.Now, End= new DateTime(2010, 3, 1, 7, 0, 0, DateTimeKind.Utc)} }
            }.ForEach(t => context.Bars.Add(t));
        }
    }
}