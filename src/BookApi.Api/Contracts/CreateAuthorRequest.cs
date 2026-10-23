namespace BookApi.Api.Contracts
{
    public record CreateAuthorRequest(
        int IDBook,
        string FirstName,
        string LastName);
}
