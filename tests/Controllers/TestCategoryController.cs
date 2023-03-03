
using AutoMapper;
using ExerciceWebApi.Controllers;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using ExerciceWebApi.Utilities.ServiceException;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace tests.Controllers
{
    public class TestCategoryController
    {

        private Mock<IBaseCrudService<Category>> _mockcategoryService = new Mock<IBaseCrudService<Category>>();

        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();


		//--GetAll tests---

		[Fact]
        public async Task CategoryController_GetCategories_ReturnOk()
        {

            var categoriesData = GetCategoriesData();
            var categoriesDtoData = GetCategoriesDtoData();

            var res = new ResponseDto("List of categories", categoriesDtoData);

            _mockcategoryService.Setup(x => x.GetAll()).ReturnsAsync(categoriesData).Verifiable();

            _mockMapper.Setup(x => x.Map<List<CategoryDto>>(categoriesData)).Returns(categoriesDtoData).Verifiable();

            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


            //act
            var response = await controller.Get() as OkObjectResult;

            //assert
            response.Value.Should().BeEquivalentTo(res);
            Assert.NotNull(response);
            response.Value.ToString().Should().Be(res.ToString());

        }

        [Fact]
        public async Task CategoryController_GetCategories_Catch_Exception()
        {

            _mockcategoryService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			await Assert.ThrowsAsync<ServiceException>(async () => await controller.Get());


		}


		[Fact]
		public async Task CategoryController_GetCategories_Return_Not_content()
		{

			var categoriesData = new List<Category>();
			var categoriesDtoData = new List<CategoryDto>();

			_mockcategoryService.Setup(x => x.GetAll()).ReturnsAsync(categoriesData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<CategoryDto>>(categoriesData)).Returns(categoriesDtoData).Verifiable();

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


			//act
			var response = await controller.Get() as NoContentResult;

			//assert
			Assert.NotNull(response);
            Assert.True(response.StatusCode == 204);

		}


		//--GetCategoryById tests---
		
        [Fact]
        public async Task CategoryController_GetCategoryById_ReturnOk()
        {

            var categoriesData = GetCategoriesData();
            var categoriesDtoData = GetCategoriesDtoData();

            var res = new ResponseDto("category", categoriesDtoData[0]);

            _mockcategoryService.Setup(x => x.GetById("1")).ReturnsAsync(categoriesData[0]).Verifiable();

            _mockMapper.Setup(x => x.Map<CategoryDto>(categoriesData[0])).Returns(categoriesDtoData[0]).Verifiable();

            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


            //act
            var response = await controller.GetCategoryById("1") as OkObjectResult;

            //assert
            response.Value.Should().BeEquivalentTo(res);
            Assert.NotNull(response);
            response.Value.ToString().Should().Be(res.ToString());

        }

        [Fact]
        public async Task CategoryController_GetCategoryById_Catch_Exception()
        {

            _mockcategoryService.Setup(x => x.GetById("")).ThrowsAsync(new Exception());

            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			await Assert.ThrowsAsync<ServiceException>(async () => await controller.GetCategoryById(""));


		}


		[Fact]
		public async Task CategoryController_GetCategoryById_Return_Not_Found_content()
		{

			Category categoriesData = null;

			_mockcategoryService.Setup(x => x.GetById("")).ReturnsAsync(categoriesData).Verifiable();

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


			//act
			var response = await controller.GetCategoryById("") as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
            Assert.True(response.StatusCode == 404);

		}



		//--CreateCategory tests---

		[Fact]
        public async Task CategoryController_CreateCategory_ReturnOk()
        {

            var categoryData = new Category() { CategoryId = "1", CategoryName = "categoria nueva", Products = null };
            var categoryDtoData = new CreateCategoryDto() { CategoryName = "categoria nueva"};

            var res = new ResponseDto("Category created");

            _mockMapper.Setup(x => x.Map<Category>(categoryDtoData)).Returns(categoryData).Verifiable();
            _mockcategoryService.Setup(x => x.Create(categoryData)).ReturnsAsync(categoryData).Verifiable();


            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


            //act
            var response = await controller.CreateCategory(categoryDtoData) as CreatedResult;

            //assert
            response.Value.Should().BeEquivalentTo(res);
            Assert.NotNull(response);
            response.StatusCode.Should().Be(201);

        }

        [Fact]
        public async Task CategoryController_CreateCategory_Catch_Exception()
        {
			Category categoryData = null;
			var categoryDtoData = new CreateCategoryDto();

			_mockcategoryService.Setup(x => x.Create(categoryData)).ThrowsAsync(new Exception());

            var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			await Assert.ThrowsAsync<ServiceException>(async () => await controller.CreateCategory(categoryDtoData));


		}


		[Fact]
		public async Task CategoryController_CreateCategory_Return_Not_Found_Content()
		{

			var categoryData = new Category() { CategoryId = "1", CategoryName = "categoria nueva", Products = null };
			var categoryDtoData = new CreateCategoryDto() { CategoryName = "categoria nueva" };

			_mockMapper.Setup(x => x.Map<Category>(categoryDtoData)).Returns(categoryData).Verifiable();
			_mockcategoryService.Setup(x => x.Create(categoryData));



			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


			//act
			var response = await controller.CreateCategory(categoryDtoData) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
            Assert.True(response.StatusCode == 404);

		}


		//--UpdateCategory tests---

		[Fact]
		public async Task CategoryController_UpdateCategory_ReturnOk()
		{

			var newcategoryData = new Category() { CategoryId = "1", CategoryName = "otra categoria", Products = null };
			var categoryDtoData = new CategoryDto() { CategoryId = "1", CategoryName = "otra categoria", Products = null };
			var categoryDtoIntoData = new CreateCategoryDto() { CategoryName = "otra categoria" };

			var res = new ResponseDto("Category updated", categoryDtoData);

			_mockMapper.Setup(x => x.Map<Category>(categoryDtoIntoData)).Returns(newcategoryData).Verifiable();
			_mockcategoryService.Setup(x => x.Update("1", newcategoryData)).ReturnsAsync(newcategoryData).Verifiable();
			_mockMapper.Setup(x => x.Map<CategoryDto>(newcategoryData)).Returns(categoryDtoData).Verifiable();


			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateCategory("1", categoryDtoIntoData) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.StatusCode.Should().Be(200);

		}

		[Fact]
		public async Task CategoryController_UpdateCategory_Catch_Exception()
		{
			Category categoryData = null;
			var categoryDtoData = new CreateCategoryDto();

			_mockcategoryService.Setup(x => x.Update("",categoryData)).ThrowsAsync(new Exception());

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			await Assert.ThrowsAsync<ServiceException>(async () => await controller.UpdateCategory("",categoryDtoData));


		}


		[Fact]
		public async Task CategoryController_UpdateCategory_Return_Not_Found_Content()
		{
			var categoryData = new Category() { CategoryId = "1", CategoryName = "categoria nueva", Products = null };
			var categoryDtoData = new CategoryDto() { CategoryId = "1", CategoryName = "categoria vieja", Products = null };
			var categoryDtoIntoData = new CreateCategoryDto() { CategoryName = "categoria vieja" };


			var res = new ResponseDto("Category not uptdated");

			_mockMapper.Setup(x => x.Map<Category>(categoryDtoIntoData)).Returns(categoryData).Verifiable();
			_mockcategoryService.Setup(x => x.Update("1", categoryData));



			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateCategory("1", categoryDtoIntoData) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.Should().BeEquivalentTo(res);

		}



		//--DeleteCategory tests---

		[Fact]
		public async Task CategoryController_DeleteCategory_ReturnOk()
		{

			var res = new ResponseDto("Category  was deleted");

			_mockcategoryService.Setup(x => x.Delete("1")).ReturnsAsync(true).Verifiable();

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			//act
			var response = await controller.DeleteCategory("1") as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.StatusCode.Should().Be(200);

		}

		[Fact]
		public async Task CategoryController_DeleteCategory_Catch_Exception()
		{

			_mockcategoryService.Setup(x => x.Delete("")).ThrowsAsync(new Exception());

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);
			//var res = await controller.DeleteCategory("");

			await Assert.ThrowsAsync<ServiceException>(async () => await controller.DeleteCategory(""));
			//Assert.Contains(res.ToString(), "Problems with your process");

		}


		[Fact]
		public async Task CategoryController_DeleteCategory_Return_Not_Found_Content()
		{

			var res = new ResponseDto("Category not found");

			_mockcategoryService.Setup(x => x.Delete("1"));

			var controller = new CategoryController(_mockcategoryService.Object, _mockMapper.Object);

			//act
			var response = await controller.DeleteCategory("1") as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.Should().BeEquivalentTo(res);

		}



		private List<Category> GetCategoriesData()
        {
            List<Category> categoriesData = new List<Category>{
                new Category { CategoryId ="1", CategoryName = "utiles escolares", Products = null},
                new Category { CategoryId ="2", CategoryName = "uniformes escolares", Products = null},
                new Category { CategoryId ="3", CategoryName = "tecnologia", Products = null},
            };
            return categoriesData;
        }

        private List<CategoryDto> GetCategoriesDtoData()
        {
            List<CategoryDto> categoriesData = new List<CategoryDto>{
                new CategoryDto { CategoryId ="1", CategoryName = "utiles escolares", Products = null},
                new CategoryDto { CategoryId ="2", CategoryName = "uniformes escolares", Products = null},
                new CategoryDto { CategoryId ="3", CategoryName = "tecnologia", Products = null},
            };
            return categoriesData;
        }
    }

}