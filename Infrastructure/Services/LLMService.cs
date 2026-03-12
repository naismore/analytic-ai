using Application.Abstract;
using Application.Models;
using System.Net.Http.Json;

namespace Infrastructure.Services;

public class LLMService(IHttpClientFactory clientFactory) : ILLMService
{
    public async Task<string> SendRequestAsync(LLMRequest request)
    {
        HttpClient httpClient = clientFactory.CreateClient("LLMClient");

        var response = await httpClient.PostAsJsonAsync("http://127.0.0.1:8000/recommend", request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
