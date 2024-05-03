using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using Prompty.Server.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prompty.Server.Models.Services
{
    public class PromptGenerationService : IOpenAIService
    {
        private readonly OpenAIService _openAiService;

        public PromptGenerationService()
        {
            _openAiService = new OpenAIService(new OpenAiOptions
            {
                ApiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY")
            });
        }

        public async Task<IEnumerable<string>> GenerateResponsesAsync(string systemPrompt, string userPrompt)
        {
            var messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemPrompt),
                ChatMessage.FromUser(userPrompt)
            };

            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = messages,
                Model = "gpt-3.5-turbo"
            });

            if (completionResult.Successful)
            {
                return completionResult.Choices.Select(choice => choice.Message.Content);
            }
            else
            {
                throw new Exception($"Failed to generate responses: {completionResult.Error.Message}");
            }
        }
    }
}
