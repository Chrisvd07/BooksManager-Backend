using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(
        int Id,
        string Title,
        string Description,
        int PageCount,
        string Excerpt,
        DateTime PublishDate) : IRequest<ErrorOr<Success>>;
}
