using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
