using BookApi.Application.DTOs;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Commands.CreateBook
{
    public record CreateBookCommand(
        string Title,
        string Description,
        int PageCount,
        string Excerpt,
        DateTime PublishDate) : IRequest<ErrorOr<BookDto>>;

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ErrorOr<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                PageCount = request.PageCount,
                Excerpt = request.Excerpt,
                PublishDate = request.PublishDate
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
    }
}
