using Microsoft.AspNetCore.Mvc;
using Prompty.Server.Models;
using Prompty.Server.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Prompty.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly IOpenAIService _openAi;

        public OpenAIController(IOpenAIService openAi, IHostEnvironment env)
        {
            _env = env;
            _openAi = openAi;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<IEnumerable<string>>> Generate([FromBody] Prompt prompt)
        {
            try
            {
                string systemPromptPath = Path.Combine(_env.ContentRootPath, prompt.RequestedPrompt);
                string systemPrompt = await System.IO.File.ReadAllTextAsync(systemPromptPath);
                var responses = await _openAi.GenerateResponsesAsync(systemPromptPath, prompt.UserPrompt);
                return Ok(responses);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound($"System prompt file not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}