using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.InputOutput;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.AspNetCore.Mvc;


namespace ExerciceWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Produces(MediaTypeNames.Application.Json)]//buscar

	public class InputOutputController : ControllerBase
	{

		private readonly IBaseCrudService<InputOutput> inputoutputService;
		private readonly IMapper mapper;
		public InputOutputController(IBaseCrudService<InputOutput> _inputoutputService, IMapper _mapper)
		{
			inputoutputService = _inputoutputService;
			mapper = _mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{

			var result = await inputoutputService.GetAll();
			var InputOutputs = mapper.Map<List<InputOutputDto>>(result);
			if (InputOutputs.Count == 0)
			{
				return NoContent();
			}
			return Ok(new ResponseDto("List of InputOutputs", InputOutputs));


		}


		[HttpPost]
		public async Task<IActionResult> CreateInputOutput(CreateInputOutputDto inputOutputDto)
		{

			var inputOutput = mapper.Map<InputOutput>(inputOutputDto);
			var result = await inputoutputService.Create(inputOutput);
			if (result == null)
			{
				return NotFound(new ResponseDto("Input or Output not create"));
			}

			return Created("", new ResponseDto("Input Output created"));

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateInputOutput([FromRoute] string id, [FromBody] CreateInputOutputDto inputOutputDto)
		{

			var inputOutput = mapper.Map<InputOutput>(inputOutputDto);
			var result = await inputoutputService.Update(id, inputOutput);
			if (result == null)
			{
				return NotFound(new ResponseDto("Input or Output not uptdated"));
			}

			var inputOutputUpdated = mapper.Map<InputOutputDto>(result);
			return Ok(new ResponseDto("Input or Output", inputOutputUpdated));

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInputOutput([FromRoute] string id)
		{

			var result = await inputoutputService.Delete(id);
			if (!result)
			{
				return NotFound(new ResponseDto("inputOutput not found"));
			}

			return Ok(new ResponseDto("inputOutput  was deleted"));

		}
	}
}