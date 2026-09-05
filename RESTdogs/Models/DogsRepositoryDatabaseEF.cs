using Microsoft.EntityFrameworkCore.Query.Internal;

namespace RESTdogs.Models
{
    public class DogsRepositoryDatabaseEF : IDogsRepository
    {
        private readonly DogsDBContext _context;

        public DogsRepositoryDatabaseEF(DogsDBContext context)
        {
            _context = context;
        }

        public Dog AddDog(Dog dog)
        {
            _context.Dogs.Add(dog);
            _context.SaveChanges();
            return dog;
        }

        public Dog? GetDogById(int id)
        {
            return _context.Dogs.Find(id);
        }

        public IEnumerable<Dog> GetDogs(
            string? nameStartsWith = null,
            int? minWeight = null,
            int? maxWeight = null,
            string? sortBy = null)
        {
            IQueryable<Dog> query = _context.Dogs.AsQueryable();

            if (nameStartsWith != null)
            {
                query = query.Where(d => d.Name != null && d.Name.StartsWith(nameStartsWith));
            }
            if (minWeight != null)
            {
                query = query.Where(d => d.Weight >= minWeight);
            }
            if (maxWeight != null)
            {
                query = query.Where(d => d.Weight <= maxWeight);
            }
            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(d => d.Name);
                    break;
                case "weight":
                    query = query.OrderBy(d => d.Weight);
                    break;
                default:
                    break;
            }
            return query;
        }

        public Dog? RemoveDog(int id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                _context.SaveChanges();
            }
            return dog;
        }

        public bool UpdateDog(int id, Dog updatedDog)
        {
            var dog = _context.Dogs.Find(id);
            if (dog == null)
            {
                return false;
            }
            dog.Name = updatedDog.Name;
            dog.Weight = updatedDog.Weight;
            _context.SaveChanges();
            return true;
        }
    }
}