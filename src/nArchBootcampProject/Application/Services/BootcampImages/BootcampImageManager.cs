using Application.Features.BootcampImages.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services.BootcampImages;

public class BootcampImageManager : IBootcampImageService
{
    private readonly IBootcampImageRepository _bootcampImageRepository;
    private readonly BootcampImageBusinessRules _bootcampImageBusinessRules;
    private readonly ImageServiceBase _imageServiceBase;

    public BootcampImageManager(IBootcampImageRepository bootcampImageRepository, 
        BootcampImageBusinessRules bootcampImageBusinessRules,
        ImageServiceBase imageServiceBase)
    {
        _bootcampImageRepository = bootcampImageRepository;
        _bootcampImageBusinessRules = bootcampImageBusinessRules;
        _imageServiceBase = imageServiceBase;
    }


    public async Task<BootcampImage> Add(IFormFile file, BootcampImageRequest request)
    {
        BootcampImage bootcampImage = new();
        {
            bootcampImage.BootcampId = request.BootcampId;
            bootcampImage.ImagePath = request.ImagePath;
        }
        bootcampImage.ImagePath = await _imageServiceBase.UploadAsync(file);
        return await _bootcampImageRepository.AddAsync(bootcampImage);
    }

    public async Task<BootcampImage> Delete(BootcampImage bootcampImage)
    {
        await _bootcampImageBusinessRules.BootcampImageIdShouldExistWhenSelected(bootcampImage.Id) ;
        var path = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot") + _bootcampImageRepository.GetAsync(c => c.Id == bootcampImage.Id).Result.ImagePath;
        var result = FileHelper.Delete(path);
        return await _bootcampImageRepository.DeleteAsync(bootcampImage);

    }

    public async Task<BootcampImage> Get(int id)
    {
        return await _bootcampImageRepository.GetAsync(b => b.Id == id);
    }

    public Task<List<BootcampImage>> GetImagesByBootcamp(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BootcampImage>> GetList()
    {
        throw new NotImplementedException();
    }

    public async Task<BootcampImage> Update(IFormFile file, BootcampImage bootcampImage)
    {
        throw new NotImplementedException();
    }
}
