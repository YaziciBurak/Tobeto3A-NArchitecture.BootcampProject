using Application.Features.BootcampImages.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Nest;

namespace Application.Features.BootcampImages.Rules;

public class BootcampImageBusinessRules : BaseBusinessRules
{
    private readonly IBootcampImageRepository _bootcampImageRepository;
    private readonly ILocalizationService _localizationService;

    public BootcampImageBusinessRules(IBootcampImageRepository bootcampImageRepository, ILocalizationService localizationService)
    {
        _bootcampImageRepository = bootcampImageRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BootcampImagesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BootcampImageShouldExistWhenSelected(BootcampImage? bootcampImage)
    {
        if (bootcampImage == null)
            await throwBusinessException(BootcampImagesBusinessMessages.BootcampImageNotExists);
    }
   /* public async Task<List<BootcampImage>> CheckIfBootcampImageNull(int bootcampId)
    {
        try
        {
            string path = @"\Images\default.jpg";
            var result = await _bootcampImageRepository.GetListAsync(predicate: c => c.Id == bootcampId);
            if (result == null)
            {
                List<BootcampImage> bootcampImages = new List<BootcampImage>();
                bootcampImages.Add(new BootcampImage { Id = bootcampId, ImagePath = path });
            }
        }
        catch (Exception e)
        {
            throw new BusinessException(e.Message);
        }
         return await _bootcampImageRepository.GetListAsync(c => c.Id == bootcampId);
       
    }*/

    public async Task BootcampImageIdShouldExistWhenSelected(int id)
    {
        BootcampImage? bootcampImage = await _bootcampImageRepository.GetAsync(
            predicate: bi => bi.Id == id,
            enableTracking: false
        );
        await BootcampImageShouldExistWhenSelected(bootcampImage);
    }
}
