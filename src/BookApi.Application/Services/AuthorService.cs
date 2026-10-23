using BookApi.Application.DTOs;
using BookApi.Application.Interfaces;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;

namespace BookApi.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                IDBook = a.IDBook,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var a = await _authorRepository.GetByIdAsync(id);
            if (a == null) return null;

            return new AuthorDto
            {
                Id = a.Id,
                IDBook = a.IDBook,
                FirstName = a.FirstName,
                LastName = a.LastName
            };
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto dto)
        {
            var author = new Author
            {
                Id = dto.Id,
                IDBook = dto.IDBook,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            var created = await _authorRepository.AddAsync(author);
            return new AuthorDto
            {
                Id = created.Id,
                IDBook = created.IDBook,
                FirstName = created.FirstName,
                LastName = created.LastName
            };
        }

        public async Task UpdateAuthorAsync(int id, AuthorDto dto)
        {
            var author = new Author
            {
                Id = dto.Id,
                IDBook = dto.IDBook,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            await _authorRepository.UpdateAsync(id, author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAsync(id);
        }
    }
}
