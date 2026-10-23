using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Commands.CreateBook
{
    public record CreateBookActionCommand(
        string Title,
        string Description,
        int PageCount,
        string Excerpt,
        DateTime PublishDate) : IRequest<ErrorOr<BookDto>>;
}
