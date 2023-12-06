using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedMango_Api.Services.Contracts;

namespace RedMango_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly ILogger<OpenAIController> _logger;
        private readonly IOpenAIService _openAIService;

        public OpenAIController(ILogger<OpenAIController> logger, IOpenAIService openAIService)
        {

            _logger = logger;
            _openAIService = openAIService;
        }

        [HttpPost]
        [Route("completeSentence")]
        public async Task<IActionResult> CompleteSentence(string text)
        {
            var result = await _openAIService.CompleteSentence(text);
            return Ok(result);
        }

        [HttpPost]
        [Route("completeSentenceAdvanced")]
        public async Task<IActionResult> CompleteSentenceAdvanced(string text)
        {
            var result = await _openAIService.CompleteSentenceAdvanced(text);
            return Ok(result);
        }

        [HttpPost]
        [Route("askQuestion")]
        public async Task<IActionResult> CheckProgrammingLanguage(string text)
        {
            var result = await _openAIService.CheckProgrammingLanguage(text);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateImage")]
        public async Task<IActionResult> CreateImages(string text)
        {
            var result = await _openAIService.CreateImage(text);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEmbeddings")]
        public async Task<IActionResult> CreateEmbeddings(string text)
        {
            var result = await _openAIService.CreateEmbeddings(text);

            return Ok(result[0].Embedding);
        }
    }
}
