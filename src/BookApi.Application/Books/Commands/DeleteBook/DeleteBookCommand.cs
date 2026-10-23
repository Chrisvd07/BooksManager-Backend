using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
