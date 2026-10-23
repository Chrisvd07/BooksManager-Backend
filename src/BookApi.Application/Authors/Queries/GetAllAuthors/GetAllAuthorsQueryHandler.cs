using BookApi.Application.DTOs;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ErrorOr<IEnumerable<AuthorDto>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<IEnumerable<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                IDBook = a.IDBook,
                FirstName = a.FirstName,
                LastName = a.LastName
            }).ToList();
        }
    }
}
