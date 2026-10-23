using BookApi.Application.Authors.Queries.GetAuthorById;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Authors.Queries
{
    public class GetAuthorByIdTests
    {
        private readonly IAuthorRepository _repository;
        private readonly GetAuthorByIdQueryHandler _handler;

        public GetAuthorByIdTests()
        {
            _repository = Substitute.For<IAuthorRepository>();
            _handler = new GetAuthorByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenAuthorExists_ShouldReturnAuthor()
        {
            // Arrange
            var authorId = 5;
            var author = new Author { Id = authorId, IDBook = 12, FirstName = "Isabel", LastName = "Allende" };
            _repository.GetByIdAsync(authorId).Returns(author);
            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(authorId);
            result.Value.FirstName.Should().Be("Isabel");
            result.Value.LastName.Should().Be("Allende");
            result.Value.IDBook.Should().Be(12);
        }

        [Fact]
        public async Task Handle_WhenAuthorDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var authorId = 999;
            _repository.GetByIdAsync(authorId).Returns((Author?)null);
            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
        }
    }
}
