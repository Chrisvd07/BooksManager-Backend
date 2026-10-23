using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, ErrorOr<Success>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Id = request.Id,
                IDBook = request.IDBook,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _authorRepository.UpdateAsync(request.Id, author);

            return Result.Success;
        }
    }
}
