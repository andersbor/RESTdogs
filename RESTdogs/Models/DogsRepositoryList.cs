namespace RESTdogs.Models
{
    public class DogsRepositoryList : IDogsRepositoryList
    {
        private List<Dog> dogs = new List<Dog>();
        private int nextId = 1;

        public IEnumerable<Dog> GetDogs()
        {
            return dogs.ToList();
        }

        public Dog? GetDog(int id)
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
            var existingDog = GetDog(id);
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
            var dog = GetDog(id);
            if (dog == null)
            {
                return null;
            }
            dogs.Remove(dog);
            return dog;
        }
    }
}