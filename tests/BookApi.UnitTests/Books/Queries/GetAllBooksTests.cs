using BookApi.Application.Books.Queries.GetAllBooks;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Books.Queries
{
    public class GetAllBooksTests
    {
        private readonly IBookRepository _repository;
        private readonly GetAllBooksQueryHandler _handler;

        public GetAllBooksTests()
        {
            _repository = Substitute.For<IBookRepository>();
            _handler = new GetAllBooksQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenBooksExist_ShouldReturnListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Cien Anos de Soledad", PageCount = 400, Excerpt = "Muchos anos despues...", PublishDate = DateTime.UtcNow },
                new() { Id = 2, Title = "El Aleph", PageCount = 200, Excerpt = "La candente manana...", PublishDate = DateTime.UtcNow }
            };
            _repository.GetAllAsync().Returns(books);
            var query = new GetAllBooksQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().HaveCount(2);
            result.Value.First().Title.Should().Be("Cien Anos de Soledad");
        }
    }
}
