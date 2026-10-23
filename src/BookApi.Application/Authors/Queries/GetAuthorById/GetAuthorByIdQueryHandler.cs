using BookApi.Application.DTOs;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, ErrorOr<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var a = await _authorRepository.GetByIdAsync(request.Id);
            if (a == null) return Error.NotFound(description: "Author not found");

            return new AuthorDto
            {
                Id = a.Id,
                IDBook = a.IDBook,
                FirstName = a.FirstName,
                LastName = a.LastName
            };
        }
    }
}
