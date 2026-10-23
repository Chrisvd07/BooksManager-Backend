using BookApi.Application.DTOs;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.CreateAuthor
{
    // El Comando
    public record CreateAuthorCommand(
        int IDBook,
        string FirstName,
        string LastName) : IRequest<ErrorOr<AuthorDto>>;

    // El Handler
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ErrorOr<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                IDBook = request.IDBook,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var created = await _authorRepository.AddAsync(author);

            return new AuthorDto
            {
                Id = created.Id,
                IDBook = created.IDBook,
                FirstName = created.FirstName,
                LastName = created.LastName
            };
        }
    }
}
