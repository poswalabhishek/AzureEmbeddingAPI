using AzureEmbeddingAPI.Models;
using AzureEmbeddingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AzureEmbeddingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbeddingController : ControllerBase
    {
        private readonly EmbeddingService _embeddingService;

        public EmbeddingController(IConfiguration configuration)
        {
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var apiKey = configuration["AzureOpenAI:ApiKey"];
            var embeddingModel = configuration["AzureOpenAI:EmbeddingModel"];
            _embeddingService = new EmbeddingService(endpoint, apiKey, embeddingModel);
        }

        [HttpPost("generate")]
        public IActionResult GenerateEmbeddings([FromBody] EmbeddingRequest request)
        {
            var result = _embeddingService.GenerateEmbeddings(request.Corps);
            return Ok(result);
        }
    }
}
