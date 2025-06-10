using APBD_Test2.DTOs;
using APBD_Test2.Models;
using APBD_Test2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APBD_Test2.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    
    public BookService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddNewBook(AddBookDTO addBookDto)
    {
        var publishingHouse = await _context.PublishingHouses
            .FirstOrDefaultAsync(ph => ph.IdPublishingHouse == addBookDto.IdPublishingHouse);
            
        if (publishingHouse == null)
        {
            throw new ArgumentException("Publishing house not found");
        }
        
        var authors = await _context.Authors
            .Where(a => addBookDto.AuthorIds.Contains(a.IdAuthor))
            .ToListAsync();
            
        if (authors.Count != addBookDto.AuthorIds.Count)
        {
            throw new ArgumentException("One or more authors not found");
        }
        
        var genreIds = new List<int>();
        foreach (var genreInput in addBookDto.Genres)
        {
            if (genreInput.IdGenre.HasValue)
            {
                var existingGenre = await _context.Genres
                    .FirstOrDefaultAsync(g => g.IdGenre == genreInput.IdGenre.Value);
                    
                if (existingGenre == null)
                {
                    throw new ArgumentException($"Genre with ID {genreInput.IdGenre} not found");
                }
                
                genreIds.Add(existingGenre.IdGenre);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(genreInput.Name))
                {
                    throw new ArgumentException("Genre name is required when ID is not provided");
                }
                
                var newGenre = new Genre { Name = genreInput.Name };
                _context.Genres.Add(newGenre);
                await _context.SaveChangesAsync();
                
                genreIds.Add(newGenre.IdGenre);
            }
        }
        
        var book = new Book
        {
            Name = addBookDto.Name,
            ReleaseDate = addBookDto.ReleaseDate,
            IdPublishingHouse = addBookDto.IdPublishingHouse
        };
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        
        foreach (var authorId in addBookDto.AuthorIds)
        {
            _context.BookAuthors.Add(new BookAuthor
            {
                IdBook = book.IdBook,
                IdAuthor = authorId
            });
        }
        
        foreach (var genreId in genreIds)
        {
            _context.BookGenres.Add(new BookGenre
            {
                IdBook = book.IdBook,
                IdGenre = genreId
            });
        }
        
        await _context.SaveChangesAsync();
        
        return book.IdBook;
    }
}