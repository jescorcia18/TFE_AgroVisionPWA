using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Extensions;
using CoffeePestDetection.Application.Features.Plot.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotsController : ControllerBase
    {
        private readonly IPlotService _service;

        public PlotsController(IPlotService service)
        {
            _service =
            service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizationId = User.GetOrganizationId();

            var result = await _service.GetPlotsAsync(organizationId);

            return Ok(ApiResponse<IReadOnlyList<PlotDto>>
                .Ok(result));
        }
    }
}
