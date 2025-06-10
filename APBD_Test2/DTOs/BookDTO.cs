namespace APBD_Test2.DTOs;

public class BookDTO
{
    public int IdBook { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<string> Authors { get; set; } = new List<string>();
    public List<string> Genres { get; set; } = new List<string>();
}