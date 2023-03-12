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
                    new User
                    {
                        Username = "sami1234",
                        Password = "12345",
                        IsAdmin = true,
                        MyBar = new Bar
                        {
                            Name = "Zona",
                            Location = "Zaragoza",
                            Capacity = 50,
                            Shows = new Show[]
                            {
                                new Show
                                {
                                    Title = "Francias - España (Eurocopa)",
                                    Start = DateTime.Now,
                                    End = DateTime.Now.AddHours(1.3),
                                    Sport = "Football"
                                },
                                new Show
                                {
                                    Title = "Francias - rwrrwrw (Eurocopa)",
                                    Start = DateTime.Now,
                                    End = DateTime.Now.AddHours(1.3),
                                    Sport = "Football"
                                },
                            }
                        }
                    },
                    new User
                    {
                        Username = "VeggieXR2",
                        Password = "543533",
                        IsAdmin = false,
                        MyBar = new Bar
                        {
                            Name = "Classic Italian",
                            Location = "Zaragoza",
                            Capacity = 50,
                            Shows = new Show[]
                            {
                                new Show
                                {
                                    Title = "Test",
                                    Start = DateTime.Now,
                                    End = DateTime.Now.AddHours(1),
                                    Sport = "Rugby"
                                },
                                new Show
                                {
                                    Title = "Test2",
                                    Start = DateTime.Now,
                                    End = DateTime.Now.AddHours(1),
                                    Sport = "Volleyball"
                                },
                            }
                        }
                    },
                    new User
                    {
                        Username = "john_doe",
                        Password = "p@ssw0rd",
                        IsAdmin = false,
                        MyBar = new Bar
                        {
                            Name = "The Sports Bar",
                            Location = "New York City",
                            Capacity = 100,
                            Shows = new Show[]
                            {
                                new Show
                                {
                                    Title = "NBA Finals",
                                    Start = DateTime.Now,
                                    End = DateTime.Now.AddHours(2),
                                    Sport = "Basketball"
                                },
                                new Show
                                {
                                    Title = "New York Yankees vs Boston Red Sox",
                                    Start = DateTime.Now.AddHours(3),
                                    End = DateTime.Now.AddHours(5),
                                    Sport = "Baseball"
                                }
                            }
                        }
                    },
                    new User
                    {
                        Username = "jane_smith",
                        Password = "p@ssw0rd123",
                        IsAdmin = true,
                        MyBar = new Bar
                        {
                            Name = "The Locker Room",
                            Location = "Los Angeles",
                            Capacity = 80,
                            Shows = new Show[]
                            {
                                new Show
                                {
                                    Title = "UFC Fight Night",
                                    Start = DateTime.Now.AddDays(2),
                                    End = DateTime.Now.AddDays(3),
                                    Sport = "Mixed Martial Arts"
                                },
                                new Show
                                {
                                    Title = "Los Angeles Lakers vs Golden State Warriors",
                                    Start = DateTime.Now.AddHours(6),
                                    End = DateTime.Now.AddHours(8),
                                    Sport = "Basketball"
                                }
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
