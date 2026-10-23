using BookApi.Application.DTOs;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Queries.GetAllBooks
{
    public record GetAllBooksQuery() : IRequest<ErrorOr<IEnumerable<BookDto>>>;
}
