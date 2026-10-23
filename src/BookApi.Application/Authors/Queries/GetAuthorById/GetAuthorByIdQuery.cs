using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Queries.GetAuthorById
{
    public record GetAuthorByIdQuery(int Id) : IRequest<ErrorOr<AuthorDto>>;
}
