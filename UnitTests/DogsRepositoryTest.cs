using RESTdogs.Models;

namespace UnitTests
{
    public class DogsRepositoryTest
    {
        private DogsRepositoryList repo;
        Dog dogBuddy = new Dog { Name = "Buddy", Weight = 30 };
        Dog dogAlly = new Dog { Name = "Ally", Weight = 25 };
        Dog dogFidel = new Dog { Name = "Fidel", Weight = 40 };

        // Constructor executed before each test method
        public DogsRepositoryTest()
        {
            repo = new DogsRepositoryList();
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
            Dog? retrievedDog = repo.GetDog(2); // Get Ally
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
            Dog? retrievedDog = repo.GetDog(1);
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
    }
}
