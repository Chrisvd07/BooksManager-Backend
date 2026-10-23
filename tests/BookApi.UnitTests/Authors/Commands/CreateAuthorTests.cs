using BookApi.Application.Authors.Commands.CreateAuthor;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Authors.Commands
{
    public class CreateAuthorTests
    {
        private readonly IAuthorRepository _repository;
        private readonly CreateAuthorCommandHandler _handler;

        public CreateAuthorTests()
        {
            _repository = Substitute.For<IAuthorRepository>();
            _handler = new CreateAuthorCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenCommandIsValid_ShouldCreateAuthor()
        {
            // Arrange
            var command = new CreateAuthorCommand(10, "Gabriel", "Garcia Marquez");
            var author = new Author
            {
                Id = 1,
                IDBook = command.IDBook,
                FirstName = command.FirstName,
                LastName = command.LastName
            };
            _repository.AddAsync(Arg.Any<Author>()).Returns(author);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(1);
            result.Value.IDBook.Should().Be(10);
            result.Value.FirstName.Should().Be("Gabriel");
            result.Value.LastName.Should().Be("Garcia Marquez");
            await _repository.Received(1).AddAsync(Arg.Any<Author>());
        }
    }
}
