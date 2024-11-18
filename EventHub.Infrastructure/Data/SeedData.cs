using EventHub.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace EventHub.Infrastructure.Data
{
    internal class SeedData
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
        public SeedData()
        {
            SeedCategories();
            SeedUsers();
            SeedEvents();
            SeedComments();
            SeedEventRegistrations();
        }

        private void SeedCategories()
        {
            Categories = new List<Category>
            {
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Music" },
                new Category { Id = 3, Name = "Art" },
                new Category { Id = 4, Name = "Sports" },
                new Category { Id = 5, Name = "Education" }
            };
        }

        private void SeedUsers()
        {
            Users = new List<User>
            {
                new User {Id = Guid.NewGuid().ToString(), Username = "john_doe", Email = "john.doe@example.com", FirstName = "John", LastName = "Doe", CreatedAt = new DateTime(2024,5,12)},
                new User {Id = Guid.NewGuid().ToString(), Username = "jane_smith", Email = "jane.smith@example.com", FirstName = "Jane", LastName = "Smith", CreatedAt = new DateTime(2024,3,5)},
                new User {Id = Guid.NewGuid().ToString(), Username = "mike_jones", Email = "mike.jones@example.com", FirstName = "Mike", LastName = "Jones", CreatedAt = new DateTime(2024,2,7)},
                new User {Id = Guid.NewGuid().ToString(), Username = "lucy_brown", Email = "lucy.brown@example.com" , FirstName = "Lucy", LastName = "Brown", CreatedAt = new DateTime(2024,1,10)},
                new User {Id = Guid.NewGuid().ToString(), Username = "susan_lee", Email = "susan.lee@example.com", FirstName = "Susan", LastName = "Lee", CreatedAt = new DateTime(2024, 10, 5)}
            };

            var passwordHasher = new PasswordHasher<User>();

            Users[0].PasswordHash = passwordHasher.HashPassword(Users[0], "john1234");
            Users[1].PasswordHash = passwordHasher.HashPassword(Users[1], "jane1234");
            Users[2].PasswordHash = passwordHasher.HashPassword(Users[2], "mike1234"); 
            Users[3].PasswordHash = passwordHasher.HashPassword(Users[3], "lucy1234");
            Users[4].PasswordHash = passwordHasher.HashPassword(Users[4], "susan1234");
        }

        private void SeedEvents()
        {
            Events = new List<Event>
            {
                new Event { Id = 1, Title = "Tech Conference", Description = "A conference on the latest in technology", Date = new DateTime(2024, 5, 15), CategoryId = 1 , Location = "Los Angeles, CA", CreatorId = Users[0].Id},
                new Event { Id = 2, Title = "Rock Concert", Description = "Live rock music concert", Date = new DateTime(2024, 6, 10), CategoryId = 2, Location = "New York, NY", CreatorId = Users[1].Id },
                new Event { Id = 3, Title = "Art Gallery Opening", Description = "New art exhibition opening", Date = new DateTime(2024, 7, 5), CategoryId = 3, Location = "Paris, France" , CreatorId = Users[2].Id },
                new Event { Id = 4, Title = "Football Tournament", Description = "Exciting football competition", Date = new DateTime(2024, 8, 20), CategoryId = 4, Location = "London, UK" , CreatorId = Users[3].Id },
                new Event { Id = 5, Title = "Online Education Webinar", Description = "A webinar about online learning", Date = new DateTime(2024, 9, 30), CategoryId = 5 , Location = "Virtual", CreatorId = Users[1].Id},
                new Event { Id = 6, Title = "AI and Machine Learning Seminar", Description = "Seminar on AI and machine learning", Date = new DateTime(2024, 10, 12), CategoryId = 1, Location = "San Francisco, CA", CreatorId = Users[4].Id },
                new Event { Id = 7, Title = "Jazz Music Night", Description = "Enjoy a night of live jazz music", Date = new DateTime(2024, 11, 22), CategoryId = 2, Location = "Chicago, IL", CreatorId = Users[2].Id },
                new Event { Id = 8, Title = "Modern Art Exhibition", Description = "Explore the world of modern art", Date = new DateTime(2024, 12, 10), CategoryId = 3, Location = "Berlin, Germany" , CreatorId = Users[3].Id },
                new Event { Id = 9, Title = "Basketball Championship", Description = "Annual basketball competition", Date = new DateTime(2024, 11, 15), CategoryId = 4, Location = "Los Angeles, CA" , CreatorId = Users[4].Id },
                new Event { Id = 10, Title = "Coding Bootcamp", Description = "Intensive coding bootcamp", Date = new DateTime(2024, 7, 10), CategoryId = 5, Location = "Austin, TX" , CreatorId = Users[1].Id},
                new Event { Id = 11, Title = "Web Development Workshop", Description = "Workshop for web development", Date = new DateTime(2024, 8, 5), CategoryId = 1 , Location = "Online", CreatorId = Users[2].Id},
                new Event { Id = 12, Title = "Pop Concert", Description = "Exciting pop concert", Date = new DateTime(2024, 10, 30), CategoryId = 2, Location = "Miami, FL", CreatorId = Users[0].Id },
                new Event { Id = 13, Title = "Digital Art Workshop", Description = "Workshop on digital art techniques", Date = new DateTime(2024, 9, 20), CategoryId = 3, Location = "Amsterdam, Netherlands" , CreatorId = Users[0].Id},
                new Event { Id = 14, Title = "Yoga Retreat", Description = "Relax and rejuvenate with yoga", Date = new DateTime(2024, 10, 1), CategoryId = 4, Location = "Bali, Indonesia" , CreatorId = Users[1].Id },
                new Event { Id = 15, Title = "Online Learning Expo", Description = "Explore the world of online learning", Date = new DateTime(2024, 11, 1), CategoryId = 5 , Location = "Virtual", CreatorId = Users[2].Id}
            };
        }

        private void SeedComments()
        {
            Comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Great event!", CreatedAt = new DateTime(2024, 6, 16), UserId = Users[0].Id, EventId = 2 },
                new Comment { Id = 2, Content = "Looking forward to it!", CreatedAt = new DateTime(2024, 5, 17), UserId = Users[1].Id, EventId = 1 },
                new Comment { Id = 3, Content = "This sounds amazing!", CreatedAt = new DateTime(2024, 8, 23), UserId = Users[2].Id, EventId = 4 },
                new Comment { Id = 4, Content = "I am going to join this!", CreatedAt = new DateTime(2024, 9, 31), UserId = Users[3].Id, EventId = 5 },
                new Comment { Id = 5, Content = "Excited to attend!", CreatedAt = new DateTime(2024, 7, 10), UserId = Users[4].Id, EventId = 3 }
            };
        }

        private void SeedEventRegistrations()
        {
            Registrations = new List<EventRegistration>
            {
                new EventRegistration { Id = 1, UserId = Users[0].Id, EventId = 1, RegisteredAt = new DateTime(2024, 5, 1) },
                new EventRegistration { Id = 2,UserId = Users[1].Id, EventId = 2, RegisteredAt = new DateTime(2024, 6, 1) },
                new EventRegistration { Id = 3, UserId = Users[2].Id, EventId = 3, RegisteredAt = new DateTime(2024, 7, 1) },
                new EventRegistration { Id = 4, UserId = Users[3].Id, EventId = 4, RegisteredAt = new DateTime(2024, 8, 1) },
                new EventRegistration { Id = 5, UserId = Users[4].Id, EventId = 5, RegisteredAt = new DateTime(2024, 9, 1) },
                new EventRegistration { Id = 6, UserId = Users[0].Id, EventId = 6, RegisteredAt = new DateTime(2024, 6, 15) },
                new EventRegistration { Id = 7, UserId = Users[1].Id, EventId = 7, RegisteredAt = new DateTime(2024, 7, 15) },
                new EventRegistration { Id = 8, UserId = Users[2].Id, EventId = 8, RegisteredAt = new DateTime(2024, 8, 15) },
                new EventRegistration { Id = 9, UserId = Users[3].Id, EventId = 9, RegisteredAt = new DateTime(2024, 9, 15) },
                new EventRegistration { Id = 10, UserId = Users[4].Id, EventId = 10, RegisteredAt = new DateTime(2024, 10, 15) },
                new EventRegistration { Id = 11, UserId = Users[0].Id, EventId = 11, RegisteredAt = new DateTime(2024, 7, 25) },
                new EventRegistration { Id = 12, UserId = Users[1].Id, EventId = 12, RegisteredAt = new DateTime(2024, 8, 25) },
                new EventRegistration { Id = 13, UserId = Users[2].Id, EventId = 13, RegisteredAt = new DateTime(2024, 9, 25) },
                new EventRegistration { Id = 14, UserId = Users[3].Id, EventId = 14, RegisteredAt = new DateTime(2024, 10, 25) },
                new EventRegistration { Id = 15, UserId = Users[4].Id, EventId = 15, RegisteredAt = new DateTime(2024, 11, 25) }
            };
        }
    }
}
