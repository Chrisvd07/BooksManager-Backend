using BookApi.Api.Contracts;
using BookApi.Application.Books.Commands.CreateBook;
using BookApi.Application.Books.Commands.DeleteBook;
using BookApi.Application.Books.Commands.UpdateBook;
using BookApi.Application.Books.Queries.GetAllBooks;
using BookApi.Application.Books.Queries.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ApiController
    {
        private readonly ISender _mediator;

        public BooksController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBooksQuery();
            var result = await _mediator.Send(query);

            return result.Match(
                books => Ok(books),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                book => Ok(book),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookRequest request)
        {
            var command = new CreateBookCommand(
                request.Title,
                request.Description,
                request.PageCount,
                request.Excerpt,
                request.PublishDate);

            var result = await _mediator.Send(command);

            return result.Match(
                createdBook => CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook),
                errors => Problem(errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookRequest request)
        {
            var command = new UpdateBookCommand(
                id,
                request.Title,
                request.Description,
                request.PageCount,
                request.Excerpt,
                request.PublishDate);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }
    }
}
