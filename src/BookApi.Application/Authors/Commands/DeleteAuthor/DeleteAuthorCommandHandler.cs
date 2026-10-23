using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ErrorOr<Deleted>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorRepository.DeleteAsync(request.Id);
            return Result.Deleted;
        }
    }
}
