using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prompty.Server.Models.Interfaces
{
    public interface IOpenAIService
    {
        Task<IEnumerable<string>> GenerateResponsesAsync(string systemPrompt, string userPrompt);
    }
}
