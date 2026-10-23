using BookApi.Application.Books.Commands.CreateBook;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Books.Commands
{
    public class CreateBookTests
    {
        private readonly IBookRepository _repository;
        private readonly CreateBookCommandHandler _handler;

        public CreateBookTests()
        {
            _repository = Substitute.For<IBookRepository>();
            _handler = new CreateBookCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenCommandIsValid_ShouldCreateBook()
        {
            // Arrange
            var command = new CreateBookCommand("Title", "Desc", 100, "Excerpt", DateTime.Now);
            var book = new Book 
            { 
                Id = 1, 
                Title = command.Title, 
                Description = command.Description,
                PageCount = command.PageCount,
                Excerpt = command.Excerpt,
                PublishDate = command.PublishDate
            };
            _repository.AddAsync(Arg.Any<Book>()).Returns(book);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(1);
            result.Value.Title.Should().Be(command.Title);
            result.Value.Description.Should().Be(command.Description);
            result.Value.PageCount.Should().Be(command.PageCount);
            result.Value.Excerpt.Should().Be(command.Excerpt);
            result.Value.PublishDate.Should().Be(command.PublishDate);
            await _repository.Received(1).AddAsync(Arg.Any<Book>());
        }
    }
}
