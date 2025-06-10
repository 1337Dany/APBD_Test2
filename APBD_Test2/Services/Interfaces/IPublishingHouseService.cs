using APBD_Test2.DTOs;
using APBD_Test2.Models;

namespace APBD_Test2.Services.Interfaces;

public interface IPublishingHouseService
{
    public Task<List<PublishingHouseDTO>> GetAllPublishingHousesAsync(string? country = null, string? city = null);
}