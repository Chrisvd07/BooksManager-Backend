using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.CreateAuthor
{
    public record CreateAuthorActionCommand(
        int IDBook,
        string FirstName,
        string LastName) : IRequest<ErrorOr<AuthorDto>>;
}
