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
    public class ProductController : ControllerBase
    {

        private readonly IBaseCrudService<Product> productService;
        private readonly IMapper mapper;

        public ProductController(IBaseCrudService<Product> _productService, IMapper _mapper)
        {
            productService = _productService;
            mapper = _mapper;

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var result = await productService.GetAll();

            if (result.Count == 0)
            {
                return NoContent();

            }
            var products = mapper.Map<List<ProductDto>>(result);

            return Ok(new ResponseDto("List products", products));




        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            var result = await productService.GetById(id);
            if (result == null)
            {
                return NotFound(new ResponseDto("Product not found"));
            }

            var product = mapper.Map<ProductDto>(result);

            return Ok(new ResponseDto("product ", product));

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {

            try
            {

                var product = mapper.Map<Product>(productDto);
                var newProduct = await productService.Create(product);
                if (newProduct == null)
                {
                    return BadRequest(new ResponseDto("Product not created"));
                }

                return Created("", new ResponseDto("Product created"));

            }
            catch (Exception ex)
            {
                throw new ServiceException("Problems with your process", ex.Message);


            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, CreateProductDto productDto)
        {
            try
            {
                var product = mapper.Map<Product>(productDto);
                var result = await productService.Update(id, product);
                if (result == null)
                {
                    return NotFound(new ResponseDto("Product not found"));
                }

                var productUpdated = mapper.Map<ProductDto>(result);

                return Ok(new ResponseDto("product  was updated", productUpdated));

            }
            catch (Exception ex)
            {
                throw new ServiceException("Problems with your process", ex.Message);


            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {

            try
            {
                var result = await productService.Delete(id);
                if (!result)
                {
                    return NotFound(new ResponseDto("Product not found"));
                }

                return Ok(new ResponseDto("product  was deleted"));

            }
            catch (Exception ex)
            {
                throw new ServiceException("Problems with your process", ex.Message);


            }

        }

    }
}