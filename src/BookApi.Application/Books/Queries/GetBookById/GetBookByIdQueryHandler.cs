using BookApi.Application.DTOs;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ErrorOr<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var b = await _bookRepository.GetByIdAsync(request.Id);
            
            if (b == null)
            {
                return Error.NotFound(description: "Book not found");
            }

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
    }
}
