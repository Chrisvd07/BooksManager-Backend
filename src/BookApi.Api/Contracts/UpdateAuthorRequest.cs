namespace BookApi.Api.Contracts
{
    public record UpdateAuthorRequest(
        int IDBook,
        string FirstName,
        string LastName);
}
