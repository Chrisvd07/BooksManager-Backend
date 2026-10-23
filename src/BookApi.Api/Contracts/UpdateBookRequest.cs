namespace BookApi.Api.Contracts
{
    public record UpdateBookRequest(
        string Title,
        string Description,
        int PageCount,
        string Excerpt,
        DateTime PublishDate);
}
