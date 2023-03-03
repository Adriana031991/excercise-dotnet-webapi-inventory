using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using ExerciceWebApi.Utilities.ServiceException;
using Microsoft.AspNetCore.Mvc;


namespace ExerciceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly IBaseCrudService<Storage> baseService;
        private readonly IStorageService storageService;
        private readonly IMapper mapper;

        public StorageController(IBaseCrudService<Storage> _baseRepository, IMapper _mapper, IStorageService _storageRepository)
        {
            baseService = _baseRepository;
            mapper = _mapper;
            storageService = _storageRepository;

        }


        [HttpGet]
        public async Task<IActionResult> GetStorages()
        {
			try
			{
				var result = await baseService.GetAll();

				var storages = mapper.Map<List<StorageDto>>(result);

				return Ok(new ResponseDto( "List storages", storages ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}


        }


        [HttpPost]
        public async Task<IActionResult> CreateStorage(CreateStorageDto storageDto)
        {
			try
			{
				var storage = mapper.Map<Storage>(storageDto);
				var newStorage = await baseService.Create(storage);
				if (newStorage == null)
				{
					return BadRequest(new ResponseDto( "Storage not created" ));
				}

				return Created("", new ResponseDto( "Storage created" ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStorage([FromRoute] string id, [FromBody] CreateStorageDto storageDto)
        {
			try
			{
				var storage = mapper.Map<Storage>(storageDto);
				var result = await baseService.Update(id, storage);
				if (result == null)
				{
					return BadRequest(new ResponseDto( "Storage not updated" ));
				}

				var storageUpdated = mapper.Map<ProductDto>(result);

				return Ok(new ResponseDto( "Storage  was updated", storageUpdated ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage([FromRoute] string id)
        {
			try
			{
				var result = await baseService.Delete(id);
				if (!result)
				{
					return NotFound(new ResponseDto( "Storage not found" ));
				}

				return Ok(new ResponseDto( "Storage  was deleted" ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorageById([FromRoute] string id)
        {
			try
			{
				var result = await baseService.GetById(id);
				if (result == null)
				{
					return NotFound(new ResponseDto( "Storage not found" ));
				}

				var storage = mapper.Map<StorageDto>(result);

				return Ok(new ResponseDto( "Storage ", storage ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			

        }

        [HttpGet("isproductinwarehouse/{id}")]
        public async Task<IActionResult> IsProductInWarehouse([FromRoute] string id)
        {
			try
			{
				var result = await storageService.IsProductInWarehouse(id);
				if (!result)
				{
					return BadRequest(new ResponseDto( "Product not in warehouse " ));
				}

				return Ok(new ResponseDto( "Product in warehouse ", result ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			


        }


        [HttpGet("storagelistbywarehouse/{id}")]
        public async Task<IActionResult> StorageListByWarehouse([FromRoute] string id)
        {
			try
			{
				var result = await storageService.StorageListByWarehouse(id);
				if (result == null)
				{
					return BadRequest(new ResponseDto( "Product not in warehouse " ));
				}

				var response = mapper.Map<List<StorageDto>>(result);

				return Ok(new ResponseDto( "Product in warehouse ", response ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }

    }
}