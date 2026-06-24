using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.IA.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/models")]
    public class AiModelController : ControllerBase
    {
        private readonly IAiModelService _service;

        public AiModelController(IAiModelService service)
        {
            _service = service;
        }

        [HttpGet("current")]
        public async Task<IActionResult>
            GetCurrent()
        {
            var result =await _service.GetCurrentModelAsync();

            return Ok(
                ApiResponse<ModelVersionDto>
                    .Ok(result));
        }

        [HttpGet("current/model-json")]
        public async Task<IActionResult>DownloadModelJson()
        {
            var file =await _service.DownloadModelJsonAsync();

            return File(
                file.Content,
                file.ContentType,
                file.FileName);
        }
    }
}


