using CoffeePestDetection.Application.Commons;
using CoffeePestDetection.Application.Features.Org.DTOs;
using CoffeePestDetection.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePestDetection.API.Controllers
{
    [ApiController]
    [Route("api/organizations")]

    public class OrganizationsController: ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult>Get()
        {
            var result =
                await _service
                    .GetOrganizationsAsync();

            return Ok(
                ApiResponse<IReadOnlyList<OrganizationDto>> //readOnly para evitar modificciones, solo lectura - DDD
                .Ok(
                    result,
                    "Organizaciones obtenidas correctamente."
                ));
        }
    }
}
