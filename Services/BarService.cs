// using WatchMe.Models;

// namespace WatchMe.Services
// {

//     public static class BarService
//     {
//         static List<Bar> Bars { get; }
//         static int nextId = 3;
//         static BarService()
//         {
//             Bars = new List<Bar>
//          {
//             new Bar { Id = 1, Name = "Classic Italian", Location = "Zaragoza",Capacity = 50, Show = new Show { Title= "Francia - España (Eurocopa)"} },
//             new Bar { Id = 2, Name = "Veggie", Location = "Pamplona",Capacity = 50, Show = new Show { Title= "Test"} }
//          };
//         }

//         public static List<Bar> GetAll() => Bars;

//         public static Bar? Get(int id) => Bars.FirstOrDefault(p => p.Id == id);

//         public static void Add(Bar Bar)
//         {
//             Bar.Id = nextId++;
//             Bars.Add(Bar);
//         }

//         public static void Delete(int id)
//         {
//             var Bar = Get(id);
//             if (Bar is null)
//                 return;

//             Bars.Remove(Bar);
//         }

//         public static void Update(Bar Bar)
//         {
//             var index = Bars.FindIndex(p => p.Id == Bar.Id);
//             if (index == -1)
//                 return;

//             Bars[index] = Bar;
//         }
//         public static bool SearchBar(string bar, string Location)
//         {
//             bool exists = false;
//             foreach (var Bar in Bars)
//             {
//                 if (Bar.Name.ToLower().Equals(bar.ToLower()) && Bar.Location.ToLower().Equals(Location.ToLower()))
//                 {
//                     exists = true;
//                 }
//             }
//             if (exists)
//             {
//                 Console.WriteLine("Sorry, that bar already exists in that location and is already registered with another account, please try again.");
//             }
//             return exists;
//         }
//     }
// }
