namespace HomeCinema.Data.Migrations
{
    using HomeCinema.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeCinemaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeCinemaContext context)
        {
            //  create genres
            context.GenreSet.AddOrUpdate(g => g.Name, GenerateGenres());

            // create movies
            context.MovieSet.AddOrUpdate(m => m.Title, GenerateMovies());

            //// create stocks
            context.StockSet.AddOrUpdate(GenerateStocks());

            // create customers
            context.CustomerSet.AddOrUpdate(GenerateCustomers());

            // create roles
            context.RoleSet.AddOrUpdate(r => r.Name, GenerateRoles());

            // username: chsakell, password: homecinema
            context.UserSet.AddOrUpdate(u => u.Email, new User[]{
                new User()
                {
                    Email="chsakells.blog@gmail.com",
                    Username="chsakell",
                    HashedPassword ="XwAQoiq84p1RUzhAyPfaMDKVgSwnn80NCtsE8dNv3XI=",
                    Salt = "mNKLRbEFCH8y1xIyTXP4qA==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                }
            });

            // // create user-admin for chsakell
            context.UserRoleSet.AddOrUpdate(new UserRole[] {
                new UserRole() {
                    RoleId = 1, // admin
                    UserId = 1  // chsakell
                }
            });
        }

        private Genre[] GenerateGenres()
        {
            Genre[] genres = new Genre[] {
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Sci-fi" },
                new Genre() { Name = "Action" },
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Crime" },
            };

            return genres;
        }
        private Movie[] GenerateMovies()
        {
            Movie[] movies = new Movie[] {
                new Movie()
                {   Title="Minions", 
                    Image ="minions.jpg", 
                    GenreId = 1, 
                    Director ="Kyle Bald", 
                    Writer="Brian Lynch", 
                    Producer="Janet Healy", 
                    ReleaseDate = new DateTime(2015, 7, 10), 
                    Rating = 3, 
                    Description = "Minions Stuart, Kevin and Bob are recruited by Scarlet Overkill, a super-villain who, alongside her inventor husband Herb, hatches a plot to take over the world.", 
                    TrailerURI = "https://www.youtube.com/watch?v=Wfql_DoHRKc" 
                },
                new Movie()
                {   Title="Ted 2", 
                    Image ="ted2.jpg", 
                    GenreId = 1, 
                    Director ="Seth MacFarlane", 
                    Writer="Seth MacFarlane", 
                    Producer="Jason Clark", 
                    ReleaseDate = new DateTime(2015, 6, 27), 
                    Rating = 4, 
                    Description = "Newlywed couple Ted and Tami-Lynn want to have a baby, but in order to qualify to be a parent, Ted will have to prove he's a person in a court of law.", 
                    TrailerURI = "https://www.youtube.com/watch?v=S3AVcCggRnU" 
                },
                new Movie()
                {   Title="Trainwreck", 
                    Image ="trainwreck.jpg", 
                    GenreId = 2, 
                    Director ="Judd Apatow", 
                    Writer="Amy Schumer", 
                    Producer="Judd Apatow", 
                    ReleaseDate = new DateTime(2015, 7, 17), 
                    Rating = 5, 
                    Description = "Having thought that monogamy was never possible, a commitment-phobic career woman may have to face her fears when she meets a good guy.", 
                    TrailerURI = "https://www.youtube.com/watch?v=2MxnhBPoIx4" 
                },
                new Movie()
                {   Title="Inside Out", 
                    Image ="insideout.jpg", 
                    GenreId = 1, 
                    Director ="Pete Docter", 
                    Writer="Pete Docter", 
                    Producer="John Lasseter", 
                    ReleaseDate = new DateTime(2015, 6, 19), 
                    Rating = 4, 
                    Description = "After young Riley is uprooted from her Midwest life and moved to San Francisco, her emotions - Joy, Fear, Anger, Disgust and Sadness - conflict on how best to navigate a new city, house, and school.", 
                    TrailerURI = "https://www.youtube.com/watch?v=seMwpP0yeu4" 
                },
                new Movie()
                {   Title="Home", 
                    Image ="home.jpg", 
                    GenreId = 1, 
                    Director ="Tim Johnson", 
                    Writer="Tom J. Astle", 
                    Producer="Suzanne Buirgy", 
                    ReleaseDate = new DateTime(2015, 5, 27), 
                    Rating = 4, 
                    Description = "Oh, an alien on the run from his own people, lands on Earth and makes friends with the adventurous Tip, who is on a quest of her own.", 
                    TrailerURI = "https://www.youtube.com/watch?v=MyqZf8LiWvM" 
                },
                new Movie()
                {   Title="Batman v Superman: Dawn of Justice", 
                    Image ="batmanvssuperman.jpg", 
                    GenreId = 2, 
                    Director ="Zack Snyder", 
                    Writer="Chris Terrio", 
                    Producer="Wesley Coller", 
                    ReleaseDate = new DateTime(2015, 3, 25), 
                    Rating = 4, 
                    Description = "Fearing the actions of a god-like Super Hero left unchecked, Gotham City's own formidable, forceful vigilante takes on Metropolis most revered, modern-day savior, while the world wrestles with what sort of hero it really needs. And with Batman and Superman at war with one another, a new threat quickly arises, putting mankind in greater danger than it's ever known before.", 
                    TrailerURI = "https://www.youtube.com/watch?v=0WWzgGyAH6Y" 
                },
                new Movie()
                {   Title="Ant-Man", 
                    Image ="antman.jpg", 
                    GenreId = 2, 
                    Director ="Payton Reed", 
                    Writer="Edgar Wright", 
                    Producer="Victoria Alonso", 
                    ReleaseDate = new DateTime(2015, 7, 17), 
                    Rating = 5, 
                    Description = "Armed with a super-suit with the astonishing ability to shrink in scale but increase in strength, cat burglar Scott Lang must embrace his inner hero and help his mentor, Dr. Hank Pym, plan and pull off a heist that will save the world.", 
                    TrailerURI = "https://www.youtube.com/watch?v=1HpZevFifuo" 
                },
                new Movie()
                {   Title="Jurassic World", 
                    Image ="jurassicworld.jpg", 
                    GenreId = 2, 
                    Director ="Colin Trevorrow", 
                    Writer="Rick Jaffa", 
                    Producer="Patrick Crowley", 
                    ReleaseDate = new DateTime(2015, 6, 12), 
                    Rating = 4, 
                    Description = "A new theme park is built on the original site of Jurassic Park. Everything is going well until the park's newest attraction--a genetically modified giant stealth killing machine--escapes containment and goes on a killing spree.", 
                    TrailerURI = "https://www.youtube.com/watch?v=RFinNxS5KN4" 
                },
                new Movie()
                {   Title="Fantastic Four", 
                    Image ="fantasticfour.jpg", 
                    GenreId = 2, 
                    Director ="Josh Trank", 
                    Writer="Simon Kinberg", 
                    Producer="Avi Arad", 
                    ReleaseDate = new DateTime(2015, 8, 7), 
                    Rating = 2, 
                    Description = "Four young outsiders teleport to an alternate and dangerous universe which alters their physical form in shocking ways. The four must learn to harness their new abilities and work together to save Earth from a former friend turned enemy.", 
                    TrailerURI = "https://www.youtube.com/watch?v=AAgnQdiZFsQ" 
                },
                new Movie()
                {   Title="Mad Max: Fury Road", 
                    Image ="madmax.jpg", 
                    GenreId = 2, 
                    Director ="George Miller", 
                    Writer="George Miller", 
                    Producer="Bruce Berman", 
                    ReleaseDate = new DateTime(2015, 5, 15), 
                    Rating = 3, 
                    Description = "In a stark desert landscape where humanity is broken, two rebels just might be able to restore order: Max, a man of action and of few words, and Furiosa, a woman of action who is looking to make it back to her childhood homeland.", 
                    TrailerURI = "https://www.youtube.com/watch?v=hEJnMQG9ev8" 
                },
                new Movie()
                {   Title="True Detective", 
                    Image ="truedetective.jpg", 
                    GenreId = 6, 
                    Director ="Nic Pizzolatto", 
                    Writer="Bill Bannerman", 
                    Producer="Richard Brown", 
                    ReleaseDate = new DateTime(2015, 5, 15), 
                    Rating = 4, 
                    Description = "An anthology series in which police investigations unearth the personal and professional secrets of those involved, both within and outside the law.", 
                    TrailerURI = "https://www.youtube.com/watch?v=TXwCoNwBSkQ" 
                },
                new Movie()
                {   Title="The Longest Ride", 
                    Image ="thelongestride.jpg", 
                    GenreId = 5, 
                    Director ="Nic Pizzolatto", 
                    Writer="George Tillman Jr.", 
                    Producer="Marty Bowen", 
                    ReleaseDate = new DateTime(2015, 5, 15), 
                    Rating = 5, 
                    Description = "After an automobile crash, the lives of a young couple intertwine with a much older man, as he reflects back on a past love.", 
                    TrailerURI = "https://www.youtube.com/watch?v=FUS_Q7FsfqU" 
                },
                new Movie()
                {   Title="The Walking Dead", 
                    Image ="thewalkingdead.jpg", 
                    GenreId = 4, 
                    Director ="Frank Darabont", 
                    Writer="David Alpert", 
                    Producer="Gale Anne Hurd", 
                    ReleaseDate = new DateTime(2015, 3, 28), 
                    Rating = 5, 
                    Description = "Sheriff's Deputy Rick Grimes leads a group of survivors in a world overrun by zombies.", 
                    TrailerURI = "https://www.youtube.com/watch?v=R1v0uFms68U" 
                },
                new Movie()
                {   Title="Southpaw", 
                    Image ="southpaw.jpg", 
                    GenreId = 3, 
                    Director ="Antoine Fuqua", 
                    Writer="Kurt Sutter", 
                    Producer="Todd Black", 
                    ReleaseDate = new DateTime(2015, 8, 17), 
                    Rating = 4, 
                    Description = "Boxer Billy Hope turns to trainer Tick Willis to help him get his life back on track after losing his wife in a tragic accident and his daughter to child protection services.", 
                    TrailerURI = "https://www.youtube.com/watch?v=Mh2ebPxhoLs" 
                },
                new Movie()
                {   Title="Specter", 
                    Image ="spectre.jpg", 
                    GenreId = 3, 
                    Director ="Sam Mendes", 
                    Writer="Ian Fleming", 
                    Producer="Zakaria Alaoui", 
                    ReleaseDate = new DateTime(2015, 11, 5), 
                    Rating = 5, 
                    Description = "A cryptic message from Bond's past sends him on a trail to uncover a sinister organization. While M battles political forces to keep the secret service alive, Bond peels back the layers of deceit to reveal the terrible truth behind SPECTRE.", 
                    TrailerURI = "https://www.youtube.com/watch?v=LTDaET-JweU" 
                },
            };

            return movies;
        }
        private Stock[] GenerateStocks()
        {
            List<Stock> stocks = new List<Stock>();

            for (int i = 1; i <= 15; i++)
            {
                // Three stocks for each movie
                for (int j = 0; j < 3; j++)
                {
                    Stock stock = new Stock()
                    {
                        MovieId = i,
                        UniqueKey = Guid.NewGuid(),
                        IsAvailable = true
                    };
                    stocks.Add(stock);
                }
            }

            return stocks.ToArray();
        }
        private Customer[] GenerateCustomers()
        {
            List<Customer> _customers = new List<Customer>();

            // Create 100 customers
            for (int i = 0; i < 100; i++)
            {
                Customer customer = new Customer()
                {
                    FirstName = MockData.Person.FirstName(),
                    LastName = MockData.Person.Surname(),
                    IdentityCard = Guid.NewGuid().ToString(),
                    UniqueKey = Guid.NewGuid(),
                    Email = MockData.Internet.Email(),
                    DateOfBirth = new DateTime(1985, 10, 20).AddMonths(i).AddDays(10),
                    RegistrationDate = DateTime.Now.AddDays(i),
                    Mobile = "1234567890"
                };

                _customers.Add(customer);
            }

            return _customers.ToArray();
        }
        private Role[] GenerateRoles()
        {
            Role[] _roles = new Role[]{
                new Role()
                {
                    Name="Admin"
                }
            };

            return _roles;
        }
        /*private Rental[] GenerateRentals()
        {
            for (int i = 1; i <= 45; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Random r = new Random();
                    int customerId = r.Next(1, 99);
                    Rental _rental = new Rental()
                    {
                        CustomerId = 1,
                        StockId = 1,
                        Status = "Returned",
                        RentalDate = DateTime.Now.AddDays(j),
                        ReturnedDate = DateTime.Now.AddDays(j + 1)
                    };

                    _rentals.Add(_rental);
                }
            }

            //return _rentals.ToArray();
        }*/
    }
}
