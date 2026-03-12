using Application.Models;

namespace Application.Abstract;

public interface ILLMService
{
    Task<string> SendRequestAsync(LLMRequest request);
}
