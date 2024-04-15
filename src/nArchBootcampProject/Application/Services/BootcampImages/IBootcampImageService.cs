using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;


namespace Application.Services.BootcampImages;

public interface IBootcampImageService
{
    Task<List<BootcampImage>> GetList();
    Task<BootcampImage> Get(int id);
    Task<BootcampImage> Add(IFormFile file, BootcampImageRequest request);
    Task<BootcampImage> Update(IFormFile file, BootcampImage bootcampImage);
    Task<BootcampImage> Delete(BootcampImage bootcampImage);
    Task<List<BootcampImage>> GetImagesByBootcamp(int id);
}
