using APBD_Test2.Models;
using APBD_Test2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Test2.Controllers;

[Route("api/publishing-houses")]
[ApiController]
public class PublishingHouseController(IPublishingHouseService publishingHouseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublishingHouse>>> GetAllPublishingHouses(
        [FromQuery] string? country,
        [FromQuery] string? city
        )
    {
        try
        {
            var houses = await publishingHouseService.GetAllPublishingHousesAsync(country, city);
            return Ok(houses);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}