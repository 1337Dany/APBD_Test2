namespace APBD_Test2.DTOs;

public class AddBookDTO
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int IdPublishingHouse { get; set; }
    public List<int> AuthorIds { get; set; } = new List<int>();
    public List<GenreInputDTO> Genres { get; set; } = new List<GenreInputDTO>();
}
