using System.Net.Http.Json;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;

namespace BookApi.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://fakerestapi.azurewebsites.net/api/v1/Authors";

        public AuthorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Author>>(BaseUrl);
            return response ?? Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Author>($"{BaseUrl}/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<Author> AddAsync(Author author)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, author);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<Author>())!;
        }

        public async Task UpdateAsync(int id, Author author)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", author);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
