using System.Net.Mime;
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
            try
            {
                var result = await categoryService.GetAll();
                var categories = mapper.Map<List<CategoryDto>>(result);
                if (categories.Count == 0)
                {
                    return NoContent();
                }
                return Ok(new ResponseDto("List of categories", categories));

            }
            catch (Exception e)
            {

                throw new ServiceException("Problems with your process", e.Message);

            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            try
            {
                var result = await categoryService.GetById(id);
                if (result == null)
                {
                    return NotFound(new ResponseDto("Category not found"));
                }
                var category = mapper.Map<CategoryDto>(result);

                return Ok(new ResponseDto("category", category));

            }
            catch (System.Exception e)
            {

                throw new ServiceException("Problems with your process", e.Message);

            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            try
            {

                var category = mapper.Map<Category>(categoryDto);
                var result = await categoryService.Create(category);
                if (result == null)
                {
                    return NotFound(new ResponseDto("Category not create"));
                }

                return Created("", new ResponseDto("Category created"));
            }
            catch (System.Exception e)
            {

                throw new ServiceException("Problems with your process", e.Message);
                ;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] CreateCategoryDto categoryDto)
        {
            try
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
            catch (System.Exception e)
            {

                throw new ServiceException("Problems with your process", e.Message);

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {

            try
            {
                var result = await categoryService.Delete(id);
                if (!result)
                {
                    return NotFound(new ResponseDto("Category not found"));
                }

                return Ok(new ResponseDto("Category  was deleted"));

            }
            catch (System.Exception e)
            {

                throw new ServiceException("Problems with your process", e.Message);

            }

        }
    }

}