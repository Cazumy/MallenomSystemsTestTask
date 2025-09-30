using ImagesApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ImagesApi.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController(DAL.Repository.ImageRepository repository) : ControllerBase
    {
        private readonly DAL.Repository.ImageRepository _repository = repository;
        [HttpGet("all")]
        public async Task<IActionResult> GetImages()
        {
            var images = await _repository.GetImages();
            var imagesDto = images
                .Select(dbImage => new ImageDto(dbImage.Id, dbImage.Name,dbImage.Format,dbImage.Data)); // Преобразование сущностей БД в DTO объекты для передачи клиенту
            // В идеале использовать AutoMapper
            return Ok(imagesDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddImage([FromBody] ImageDto imageDto)
        {
            if (imageDto == null)
                return BadRequest();

            var dbImage = new DAL.DbEntities.Image(0, imageDto.Name, imageDto.Format, imageDto.Data);
            var addedImage = await _repository.AddImage(dbImage);                                          // Добавление объекта в БД

            var result = new ImageDto(addedImage.Id, addedImage.Name, addedImage.Format, addedImage.Data); // DTO объект с присвоенным ID для возвращения клиенту
            return Ok(result);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateImage(int id, ImageDto imageDto)
        {
            if (imageDto == null)
                return BadRequest();

            if (await _repository.GetImage(id) == null)
                return NotFound();

            var dbImage = new DAL.DbEntities.Image(id, imageDto.Name, imageDto.Format, imageDto.Data);
            await _repository.UpdateImage(dbImage);
            return Ok(imageDto);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _repository.DeleteImage(id);
            return NoContent();
        }
    }
}
