using BookApi.Application.DTOs;

namespace BookApi.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        Task UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task DeleteAuthorAsync(int id);
    }
}
