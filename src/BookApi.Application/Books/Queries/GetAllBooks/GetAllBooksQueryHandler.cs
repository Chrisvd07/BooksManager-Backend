using BookApi.Application.DTOs;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ErrorOr<IEnumerable<BookDto>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
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
            }).ToList();
        }
    }
}
