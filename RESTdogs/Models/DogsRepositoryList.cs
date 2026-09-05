namespace RESTdogs.Models
{
    public class DogsRepositoryList : IDogsRepository
    {
        private List<Dog> dogs = new List<Dog>();
        private int nextId = 1;

        public IEnumerable<Dog> GetDogs(string? nameStartsWith = null,
            int? minWeight = null,
            int? maxWeight = null,
            string? sortBy = null)
        {
            return dogs.ToList();
        }

        public Dog? GetDogById(int id)
        {
            return dogs.FirstOrDefault(d => d.Id == id);
        }

        public Dog AddDog(Dog dog)
        {
            dog.Id = nextId++;
            dogs.Add(dog);
            return dog;
        }

        public bool UpdateDog(int id, Dog updatedDog)
        {
            var existingDog = GetDogById(id);
            if (existingDog == null)
            {
                return false;
            }
            existingDog.Name = updatedDog.Name;
            existingDog.Weight = updatedDog.Weight;
            return true;
        }

        public Dog? RemoveDog(int id)
        {
            var dog = GetDogById(id);
            if (dog == null)
            {
                return null;
            }
            dogs.Remove(dog);
            return dog;
        }
    }
}