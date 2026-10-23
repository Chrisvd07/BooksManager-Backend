using BookApi.Application.Books.Queries.GetBookById;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Books.Queries
{
    public class GetBookByIdTests
    {
        private readonly IBookRepository _repository;
        private readonly GetBookByIdQueryHandler _handler;

        public GetBookByIdTests()
        {
            _repository = Substitute.For<IBookRepository>();
            _handler = new GetBookByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenBookExists_ShouldReturnBook()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book" };
            _repository.GetByIdAsync(bookId).Returns(book);
            var query = new GetBookByIdQuery(bookId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(bookId);
            result.Value.Title.Should().Be("Test Book");
        }

        [Fact]
        public async Task Handle_WhenBookDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var bookId = 99;
            _repository.GetByIdAsync(bookId).Returns((Book?)null);
            var query = new GetBookByIdQuery(bookId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
        }
    }
}
