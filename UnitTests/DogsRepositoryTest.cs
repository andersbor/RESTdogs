using Microsoft.EntityFrameworkCore;
using RESTdogs.Models;

namespace UnitTests
{
    public class DogsRepositoryTest
    {
        private bool useDatabase = true; // Set to true to test with database, false for in-memory list
        private bool useInMemoryDatabase = false; // Set to true to use in-memory database for testing

        private IDogsRepository repo;
        Dog dogBuddy = new Dog { Name = "Buddy", Weight = 30 };
        Dog dogAlly = new Dog { Name = "Ally", Weight = 25 };
        Dog dogFidel = new Dog { Name = "Fidel", Weight = 40 };

        // Constructor executed before each test method
        public DogsRepositoryTest()
        {
            if (useDatabase)
            {
                if (useInMemoryDatabase)
                {
                    // Setup in-memory database for testing
                    DbContextOptions<DogsDBContext> options = new DbContextOptionsBuilder<DogsDBContext>()
                        .UseInMemoryDatabase(databaseName: "DogsTestDB")
                        .Options;
                    DogsDBContext context = new DogsDBContext(options);
                    context.Database.EnsureDeleted(); // Ensure a clean state for each test
                    context.Database.EnsureCreated();
                    repo = new DogsRepositoryDatabaseEF(context);
                }
                else
                {
                    // Setup real database connection for testing (not recommended for unit tests)
                    var optionsBuilder = new DbContextOptionsBuilder<DogsDBContext>();
                    optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                    DogsDBContext context = new DogsDBContext(optionsBuilder.Options);
                    context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Dogs"); // Clean up the database before each test
                    repo = new DogsRepositoryDatabaseEF(context);
                }
            }
            else
            {
                repo = new DogsRepositoryList();
            }
        }

        [Fact]
        public void TestEmptyRepository()
        {
            Assert.Empty(repo.GetDogs());
        }

        [Fact]
        public void TestAddDog()
        {
            Dog addedDog = repo.AddDog(dogBuddy);
            Assert.NotNull(addedDog);
            Assert.Equal(1, addedDog.Id);
            Assert.Equal("Buddy", addedDog.Name);
            Assert.Equal(30, addedDog.Weight);

            Assert.Single(repo.GetDogs());
        }

        [Fact]
        public void TestGetById()
        {
            repo.AddDog(dogBuddy);
            repo.AddDog(dogAlly);
            Dog? retrievedDog = repo.GetDogById(2); // Get Ally
            Assert.NotNull(retrievedDog);
            Assert.Equal("Ally", retrievedDog.Name);
            Assert.Equal(25, retrievedDog.Weight);
        }

        [Fact]
        public void TestRemoveDog()
        {
            repo.AddDog(dogBuddy);
            repo.AddDog(dogAlly);
            repo.AddDog(dogFidel);
            Dog? deletedDog = repo.RemoveDog(2); // Remove Ally
            Assert.NotNull(deletedDog);
            Assert.Equal("Ally", deletedDog.Name);
            Assert.Equal(25, deletedDog.Weight);
            Assert.Equal(2, repo.GetDogs().Count());
        }

        [Fact]
        public void TestRemoveNonExistentDog()
        {
            repo.AddDog(dogBuddy);
            Dog? deletedDog = repo.RemoveDog(99); // Non-existent ID
            Assert.Null(deletedDog);
            Assert.Single(repo.GetDogs());
        }

        [Fact]
        public void TestUpdateDog()
        {
            repo.AddDog(dogBuddy);
            Dog updatedDog = new Dog { Name = "BuddyUpdated", Weight = 35 };
            bool updateResult = repo.UpdateDog(1, updatedDog);
            Assert.True(updateResult);
            Dog? retrievedDog = repo.GetDogById(1);
            Assert.NotNull(retrievedDog);
            Assert.Equal("BuddyUpdated", retrievedDog.Name);
            Assert.Equal(35, retrievedDog.Weight);
        }

        [Fact]
        public void TestUpdateNonExistentDog()
        {
            Dog updatedDog = new Dog { Name = "NonExistent", Weight = 50 };
            bool updateResult = repo.UpdateDog(99, updatedDog); // Non-existent ID
            Assert.False(updateResult);
        }

        [Fact]
        public void TestGetDogsWithFilters()
        {
            repo.AddDog(dogBuddy);
            repo.AddDog(dogAlly);
            repo.AddDog(dogFidel);
            var filteredDogs = repo.GetDogs(nameStartsWith: "B", minWeight: 20, maxWeight: 35, sortBy: "weight");
            Assert.Single(filteredDogs);
            Assert.Equal("Buddy", filteredDogs.First().Name);
        }
    }
}
