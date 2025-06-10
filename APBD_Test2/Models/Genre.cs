namespace APBD_Test2.Models;

public class Genre
{
    public int IdGenre { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}