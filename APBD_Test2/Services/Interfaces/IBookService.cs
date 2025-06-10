using APBD_Test2.DTOs;
using APBD_Test2.Models;

namespace APBD_Test2.Services.Interfaces;

public interface IBookService
{
    public Task<int> AddNewBookAsync(AddBookDTO addBookDto);
}