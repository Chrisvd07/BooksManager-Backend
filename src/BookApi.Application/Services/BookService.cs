using BookApi.Application.DTOs;
using BookApi.Application.Interfaces;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;

namespace BookApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                PageCount = b.PageCount,
                Excerpt = b.Excerpt,
                PublishDate = b.PublishDate
            });
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var b = await _bookRepository.GetByIdAsync(id);
            if (b == null) return null;

            return new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                PageCount = b.PageCount,
                Excerpt = b.Excerpt,
                PublishDate = b.PublishDate
            };
        }

        public async Task<BookDto> CreateBookAsync(BookDto dto)
        {
            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                PageCount = dto.PageCount,
                Excerpt = dto.Excerpt,
                PublishDate = dto.PublishDate
            };
            var created = await _bookRepository.AddAsync(book);
            return new BookDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                PageCount = created.PageCount,
                Excerpt = created.Excerpt,
                PublishDate = created.PublishDate
            };
        }

        public async Task UpdateBookAsync(int id, BookDto dto)
        {
            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                PageCount = dto.PageCount,
                Excerpt = dto.Excerpt,
                PublishDate = dto.PublishDate
            };
            await _bookRepository.UpdateAsync(id, book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
