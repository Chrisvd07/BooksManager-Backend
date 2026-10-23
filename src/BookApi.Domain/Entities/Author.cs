namespace BookApi.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public int IDBook { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
