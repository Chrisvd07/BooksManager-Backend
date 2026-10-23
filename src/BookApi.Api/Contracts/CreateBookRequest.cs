namespace BookApi.Api.Contracts
{
    public record CreateBookRequest(
        string Title,
        string Description,
        int PageCount,
        string Excerpt,
        DateTime PublishDate);
}
