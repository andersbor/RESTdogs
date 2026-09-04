namespace RESTdogs.Models
{
    public interface IDogsRepositoryList
    {
        Dog AddDog(Dog dog);
        Dog? GetDog(int id);
        IEnumerable<Dog> GetDogs();
        Dog? RemoveDog(int id);
        bool UpdateDog(int id, Dog updatedDog);
    }
}