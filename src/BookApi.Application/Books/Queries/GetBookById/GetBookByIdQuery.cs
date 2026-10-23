using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id) : IRequest<ErrorOr<BookDto>>;
}
