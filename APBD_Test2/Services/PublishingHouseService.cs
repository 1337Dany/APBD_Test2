using APBD_Test2.DTOs;
using APBD_Test2.Models;
using APBD_Test2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APBD_Test2.Services;

public class PublishingHouseService : IPublishingHouseService
{
    private readonly AppDbContext _context;

    public PublishingHouseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PublishingHouseDTO>> GetAllPublishingHousesAsync(string? country, string? city)
    {
        var query = _context.PublishingHouses
            .Include(ph => ph.Books)
            .ThenInclude(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .Include(ph => ph.Books)
            .ThenInclude(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .AsQueryable();

        if (!string.IsNullOrEmpty(country))
        {
            query = query.Where(ph => ph.Country == country);
        }

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(ph => ph.City == city);
        }

        var publishingHouses = await query
            .OrderBy(ph => ph.Country)
            .ThenBy(ph => ph.Name)
            .ToListAsync();

        return publishingHouses.Select(ph => new PublishingHouseDTO
        {
            IdPublishingHouse = ph.IdPublishingHouse,
            Name = ph.Name,
            Country = ph.Country,
            City = ph.City,
            Books = ph.Books.Select(b => new BookDTO
            {
                IdBook = b.IdBook,
                Name = b.Name,
                ReleaseDate = b.ReleaseDate,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}").ToList(),
                Genres = b.BookGenres.Select(bg => bg.Genre.Name).ToList()
            }).ToList()
        }).ToList();
    }
}