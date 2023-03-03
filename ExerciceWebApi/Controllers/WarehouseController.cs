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
    public class Warehousecontroller : ControllerBase
    {

        private readonly IBaseCrudService<Warehouse> warehouseService;
        private readonly IMapper mapper;


        public Warehousecontroller(IBaseCrudService<Warehouse> _warehouseService, IMapper _mapper)
        {
            warehouseService = _warehouseService;
            mapper = _mapper;

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
			try
			{
				var result = await warehouseService.GetAll();

				var response = mapper.Map<List<WarehouseDto>>(result);

				return Ok(new ResponseDto("List Warehouses", response ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }


        [HttpPost]
        public async Task<IActionResult> CreateWarehouse(CreateWarehouseDto warehouseDto)
        {
			try
			{
				var warehouse = mapper.Map<Warehouse>(warehouseDto);
				var newWarehouse = await warehouseService.Create(warehouse);
				if (newWarehouse == null)
				{
					return BadRequest(new ResponseDto("Warehouse not created" ));
				}

				return Created("", new ResponseDto("Warehouse created" ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(string id, CreateWarehouseDto warehouseDto)
        {
			try
			{
				var warehouse = mapper.Map<Warehouse>(warehouseDto);
				var result = await warehouseService.Update(id, warehouse);
				if (result == null)
				{
					return BadRequest(new ResponseDto("Warehouse not updated" ));
				}

				var response = mapper.Map<WarehouseDto>(result);

				return Ok(new ResponseDto("Warehouse  was updated", response ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(string id)
        {
			try
			{
				var result = await warehouseService.Delete(id);
				if (!result)
				{
					return NotFound(new ResponseDto("Warehouse not found" ));
				}

				return Ok(new ResponseDto("Warehouse  was deleted" ));

			}
			catch (Exception e)
			{
				throw new ServiceException("Problems with your process", e.Message);

			}
			
        }
    }
}