using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookApi.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ErrorOr<Success>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                PageCount = request.PageCount,
                Excerpt = request.Excerpt,
                PublishDate = request.PublishDate
            };

            await _bookRepository.UpdateAsync(request.Id, book);

            return Result.Success;
        }
    }
}
