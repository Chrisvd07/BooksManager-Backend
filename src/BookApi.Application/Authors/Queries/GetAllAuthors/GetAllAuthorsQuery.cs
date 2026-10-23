using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Queries.GetAllAuthors
{
    public record GetAllAuthorsQuery() : IRequest<ErrorOr<IEnumerable<AuthorDto>>>;
}
