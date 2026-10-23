using BookApi.Api.Contracts;
using BookApi.Application.Authors.Commands.CreateAuthor;
using BookApi.Application.Authors.Commands.DeleteAuthor;
using BookApi.Application.Authors.Commands.UpdateAuthor;
using BookApi.Application.Authors.Queries.GetAllAuthors;
using BookApi.Application.Authors.Queries.GetAuthorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : ApiController
    {
        private readonly ISender _mediator;

        public AuthorsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAuthorsQuery();
            var result = await _mediator.Send(query);

            return result.Match(
                authors => Ok(authors),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                author => Ok(author),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorRequest request)
        {
            var command = new CreateAuthorCommand(
                request.IDBook,
                request.FirstName,
                request.LastName);

            var result = await _mediator.Send(command);

            return result.Match(
                createdAuthor => CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor),
                errors => Problem(errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorRequest request)
        {
            var command = new UpdateAuthorCommand(
                id,
                request.IDBook,
                request.FirstName,
                request.LastName);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }
    }
}
