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
               Fixture fixture = new Fixture();
               fixture.Customize<User>(User => User.Without(p => p.Id));
               //--- The next two lines add 100 rows to your database
               List<User> Users = fixture.CreateMany<User>(10).ToList();
               dbContext.AddRange(Users);
               dbContext.SaveChanges();
          }
       }
   }
}
