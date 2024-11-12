using iserra_api.Dto;
using iserra_api.Responses;

namespace iserra_api.Services.AdvertServices {
    public interface IAdvertService {
        Task<ResponseModel<List<AdvertDto>>> GetAll();
        Task<ResponseModel<AdvertDto>> GetWithId(Guid id);
        Task<ResponseModel<AdvertDto>> Create(AdvertCreateDto advert);
        Task<ResponseModel<AdvertDto>> Update(AdvertUpdateDto advertUpdateDto, Guid id);
        Task<ResponseModel<AdvertDto>> Delete(Guid id);
    }
}
