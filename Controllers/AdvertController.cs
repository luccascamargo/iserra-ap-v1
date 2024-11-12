using iserra_api.Dto;
using iserra_api.Services.AdvertServices;
using Microsoft.AspNetCore.Mvc;

namespace iserra_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {

        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        [HttpGet("/adverts")]
        public async Task<ActionResult<List<AdvertDto>>> GetAll()
        {

            var adverts = await _advertService.GetAll();

            return Ok(adverts);
        }

        [HttpPost("/adverts")]
        public async Task<ActionResult<AdvertDto>> Create([FromBody] AdvertCreateDto advertCreateDto)
        {
            var advert = await _advertService.Create(advertCreateDto);

            return Created("Anuncio criado com sucesso", advert);

        }

        [HttpGet("/adverts/{id}")]
        public async Task<ActionResult<AdvertDto>> GetWithId(Guid id)
        {
            var advert = await _advertService.GetWithId(id);

            return Ok(advert);
        }

        [HttpDelete("/adverts/{id}")]
        public async Task<ActionResult<AdvertDto>> Delete(Guid id) {
            
            await _advertService.Delete(id);

            return Ok();
        }

        [HttpPut("/adverts/{id}")]
        public async Task<ActionResult<AdvertDto>> Update([FromBody] AdvertUpdateDto advertUpdateDto, Guid id) {

            await _advertService.Update(advertUpdateDto,id);

            return Ok();
        }
    }
}
