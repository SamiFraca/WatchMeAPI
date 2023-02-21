using WatchMe.Models;

namespace WatchMe.Services
{

    public static class UserService
    {
        static List<User> Users { get; }
        static int nextId = 3;
        static UserService()
        {
            Users = new List<User>
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
        };
        }

        public static List<User> GetAll() => Users;

        public static User? Get(int id) => Users.FirstOrDefault(p => p.Id == id);

        public static void Add(User User)
        {
            User.Id = nextId++;
            Users.Add(User);
        }

        public static void Delete(int id)
        {
            var User = Get(id);
            if (User is null)
                return;

            Users.Remove(User);
        }

        public static void Update(User User)
        {
            var index = Users.FindIndex(p => p.Id == User.Id);
            if (index == -1)
                return;

            Users[index] = User;
        }
        // public static bool SearchUser(string User, string location)
        // {
        //     bool exists = false;
        //     foreach (var User in Users)
        //     {
        //         if (User.Username.ToLower().Equals(User.ToLower()) && User.location.ToLower().Equals(location.ToLower()))
        //         {
        //             exists = true;
        //         }
        //     }
        //     if (exists)
        //     {
        //         Console.WriteLine("Sorry, that User already exists in that location and is already registered with another account, please try again.");
        //     }
        //     return exists;
        // }
    }
}