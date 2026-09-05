namespace RESTdogs.Models
{
    public interface IDogsRepository
    {
        Dog AddDog(Dog dog);
        Dog? GetDogById(int id);
        IEnumerable<Dog> GetDogs(string? nameStartsWith = null,
            int? minWeight = null,
            int? maxWeight = null,
            string? sortBy = null);
        Dog? RemoveDog(int id);
        bool UpdateDog(int id, Dog updatedDog);
    }
}