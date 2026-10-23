using BookApi.Domain.Entities;

namespace BookApi.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
    }
}
