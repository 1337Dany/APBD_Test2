namespace APBD_Test2.DTOs;

public class PublishingHouseDTO
{
    public int IdPublishingHouse { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public List<BookDTO> Books { get; set; } = new List<BookDTO>();
}