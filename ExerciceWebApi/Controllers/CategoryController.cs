using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace ExerciceWebApi.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	//[Produces(MediaTypeNames.Application.Json)]//buscar

	public class CategoryController : ControllerBase
	{

		private readonly IBaseCrudService<Category> categoryService;
		private readonly IMapper mapper;
		public CategoryController(IBaseCrudService<Category> _categoryService, IMapper _mapper)
		{
			categoryService = _categoryService;
			mapper = _mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{

			var result = await categoryService.GetAll();
			var categories = mapper.Map<List<CategoryDto>>(result); //debe ir service
			if (categories.Count == 0)
			{
				return NoContent();//cambio a notfound
			}
			return Ok(new ResponseDto("List of categories", categories));

		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] string id)
		{

			var result = await categoryService.GetById(id);
			if (result == null)
			{
				return NotFound(new ResponseDto("Category not found"));
			}
			var category = mapper.Map<CategoryDto>(result);

			return Ok(new ResponseDto("category", category));


		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
		{

			var category = mapper.Map<Category>(categoryDto);
			var result = await categoryService.Create(category);
			if (result == null)
			{
				return NotFound(new ResponseDto("Category not create"));
			}

			return Created("", new ResponseDto("Category created"));

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] CreateCategoryDto categoryDto)
		{

			var category = mapper.Map<Category>(categoryDto);
			var result = await categoryService.Update(id, category);
			if (result == null)
			{
				return NotFound(new ResponseDto("Category not uptdated"));
			}

			var categoryUpdated = mapper.Map<CategoryDto>(result);
			return Ok(new ResponseDto("Category updated", categoryUpdated));

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] string id)
		{

			var result = await categoryService.Delete(id);
			if (!result)
			{
				return NotFound(new ResponseDto("Category not found"));
			}

			return Ok(new ResponseDto("Category  was deleted"));

		}
	}

}