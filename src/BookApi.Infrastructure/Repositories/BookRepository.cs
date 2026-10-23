using System.Net.Http.Json;
using BookApi.Domain.Entities;
using BookApi.Domain.Interfaces;

namespace BookApi.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://fakerestapi.azurewebsites.net/api/v1/Books";

        public BookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>(BaseUrl);
            return response ?? Enumerable.Empty<Book>();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Book>($"{BaseUrl}/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<Book> AddAsync(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, book);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<Book>())!;
        }

        public async Task UpdateAsync(int id, Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", book);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
