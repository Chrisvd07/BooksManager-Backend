using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.UpdateAuthor
{
    public record UpdateAuthorCommand(
        int Id,
        int IDBook,
        string FirstName,
        string LastName) : IRequest<ErrorOr<Success>>;
}
