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
        private readonly IOpenAIService _openAi;

        public OpenAIController(IOpenAIService openAi)
        {
            _openAi = openAi;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<IEnumerable<string>>> Generate([FromBody] Prompt promptRequest)
        {
            try
            {
                string systemPrompt = await System.IO.File.ReadAllTextAsync("./gen.txt");
                Console.WriteLine(systemPrompt);
                var responses = await _openAi.GenerateResponsesAsync(systemPrompt, promptRequest.UserPrompt);
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