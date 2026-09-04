namespace RESTdogs.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"Dog: {Name}, Weight: {Weight}";
        }
    }
}
