using BookApi.Application.Authors.Queries.GetAllAuthors;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookApi.UnitTests.Authors.Queries
{
    public class GetAllAuthorsTests
    {
        private readonly IAuthorRepository _repository;
        private readonly GetAllAuthorsQueryHandler _handler;

        public GetAllAuthorsTests()
        {
            _repository = Substitute.For<IAuthorRepository>();
            _handler = new GetAllAuthorsQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_WhenAuthorsExist_ShouldReturnListOfAuthors()
        {
            // Arrange
            var authors = new List<Author>
            {
                new() { Id = 1, IDBook = 10, FirstName = "Mario", LastName = "Vargas Llosa" },
                new() { Id = 2, IDBook = 20, FirstName = "Jorge Luis", LastName = "Borges" }
            };
            _repository.GetAllAsync().Returns(authors);
            var query = new GetAllAuthorsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().HaveCount(2);
            result.Value.First().FirstName.Should().Be("Mario");
        }
    }
}
