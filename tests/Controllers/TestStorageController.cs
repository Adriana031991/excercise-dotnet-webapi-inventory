using AutoMapper;
using ExerciceWebApi.Controllers;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Storage = ExerciceWebApi.Models.Entities.Storage;
using FluentAssertions;
using Microsoft.Identity.Client.Extensions.Msal;

namespace tests.Controllers
{
	public class TestStorageController
	{
		private Mock<IBaseCrudService<Storage>> _mockStorageBaseService;
		private Mock<IStorageService> _mockStorageService;

		private readonly Mock<IMapper> _mockMapper;

		public TestStorageController()
		{
			_mockStorageBaseService = new Mock<IBaseCrudService<Storage>>();
			_mockMapper = new Mock<IMapper>();
			_mockStorageService = new Mock<IStorageService>();
		}


		//--GetAll tests---

		[Fact]
		public async Task StorageController_GetStorgages_ReturnOk()
		{

			var storagesData = new StorageBuilder().Build();
			var storagesDtoData = new StorageDtoBuilder().Build();

			var res = new ResponseDto("List storages", storagesDtoData);

			_mockStorageBaseService.Setup(x => x.GetAll()).ReturnsAsync(storagesData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<StorageDto>>(storagesData)).Returns(storagesDtoData).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.GetStorages() as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.Value.ToString().Should().Be(res.ToString());

		}

		//[Fact]
		//public async Task CategoryController_GetStorgages_Catch_Exception()
		//{

		//	_mockStorageBaseService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
			
		//	var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.GetStorages());


		//}


		[Fact]
		public async Task StorageController_GetStorgages_Return_Not_content()
		{

			var categoriesData = new List<Storage>();
			var categoriesDtoData = new List<StorageDto>();

			_mockStorageBaseService.Setup(x => x.GetAll()).ReturnsAsync(categoriesData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<StorageDto>>(categoriesData)).Returns(categoriesDtoData).Verifiable();
			
			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);



			//act
			var response = await controller.GetStorages() as NoContentResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 204);

		}


		//--CreateStorage tests---

		[Fact]
		public async Task StorageController_CreateStorage_ReturnOk()
		{

			var storagesData = new StorageBuilder().Build();
			var storagesDtoData = new StorageDtoBuilder().Build();

			var res = new ResponseDto("Storage created");

			_mockStorageBaseService.Setup(x => x.Create(It.IsAny<Storage>())).ReturnsAsync(storagesData[0]).Verifiable();

			_mockMapper.Setup(x => x.Map<StorageDto>(It.IsAny<Storage>())).Returns(storagesDtoData[0]).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.CreateStorage(It.IsAny<CreateStorageDto>()) as CreatedResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(201, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}

		//[Fact]
		//public async Task CategoryController_CreateStorage_Catch_Exception()
		//{

		//	_mockStorageBaseService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

		//	var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.GetStorages());


		//}


		[Fact]
		public async Task StorageController_CreateStorage_Return_BadRequest()
		{

			var categoriesData = new List<Storage>();
			var categoriesDtoData = new List<StorageDto>();
			var res = new ResponseDto("Storage not created");

			_mockMapper.Setup(x => x.Map<Storage>(It.IsAny<CreateStorageDto>())).Returns(It.IsAny<Storage>()).Verifiable();
			_mockStorageBaseService.Setup(x => x.Create(It.IsAny<Storage>()));


			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);



			//act
			var response = await controller.CreateStorage(It.IsAny<CreateStorageDto>()) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.ToString().Should().Be(res.ToString());

		}


		//--UpdateStorage tests---

		[Fact]
		public async Task StorageController_UpdateStorage_ReturnOk()
		{
			//assert
			var storagesData = new StorageBuilder().Build();
			var storagesDtoData = new StorageDtoBuilder().Build();

			var res = new ResponseDto("Storage  was updated", storagesDtoData[0]);

			_mockMapper.Setup(x => x.Map<Storage>(It.IsAny<CreateStorageDto>())).Returns(storagesData[0]).Verifiable();
			_mockStorageBaseService.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Storage>())).ReturnsAsync(storagesData[0]).Verifiable();
			_mockMapper.Setup(x => x.Map<StorageDto>(It.IsAny<Storage>())).Returns(storagesDtoData[0]).Verifiable();


			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.UpdateStorage(It.IsAny<string>(), It.IsAny<CreateStorageDto>()) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(200, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}


		[Fact]
		public async Task StorageController_UpdateStorage_Return_BadRequest()
		{

			var res = new ResponseDto("Storage not updated");

			_mockMapper.Setup(x => x.Map<Storage>(It.IsAny<CreateStorageDto>())).Returns(It.IsAny<Storage>()).Verifiable();
			_mockStorageBaseService.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Storage>()));


			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);



			//act
			var response = await controller.UpdateStorage(It.IsAny<string>(),It.IsAny<CreateStorageDto>()) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.ToString().Should().Be(res.ToString());

		}


		//--DeleteStorage tests---

		[Fact]
		public async Task StorageController_DeleteStorage_ReturnOk()
		{
			//assert
			var res = new ResponseDto("Storage  was deleted");
			_mockStorageBaseService.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.DeleteStorage(It.IsAny<string>()) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(200, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}


		[Fact]
		public async Task StorageController_DeleteStorage_Return_BadRequest()
		{

			var res = new ResponseDto("Storage not found");

			_mockStorageBaseService.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.DeleteStorage(It.IsAny<string>()) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.ToString().Should().Be(res.ToString());

		}

		
		//--GetStorageById tests---

		[Fact]
		public async Task StorageController_GetStorageById_ReturnOk()
		{
			//assert
			var storagesData = new StorageBuilder().Build();
			var storagesDtoData = new StorageDtoBuilder().Build();

			var res = new ResponseDto("Storage ", storagesDtoData[0]);

			_mockStorageBaseService.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(storagesData[0]).Verifiable();
			_mockMapper.Setup(x => x.Map<StorageDto>(It.IsAny<Storage>())).Returns(storagesDtoData[0]).Verifiable();


			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.GetStorageById(It.IsAny<string>()) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(200, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}


		[Fact]
		public async Task StorageController_GetStorageById_Return_BadRequest()
		{

			var res = new ResponseDto("Storage not found");

			_mockStorageBaseService.Setup(x => x.GetById(It.IsAny<string>()));

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.GetStorageById(It.IsAny<string>()) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.ToString().Should().Be(res.ToString());

		}


		//--IsProductInWarehouse tests---

		[Fact]
		public async Task StorageController_IsProductInWarehouse_ReturnOk()
		{
			//assert
			
			var res = new ResponseDto("Product in warehouse ", true);

			_mockStorageService.Setup(x => x.IsProductInWarehouse(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.IsProductInWarehouse(It.IsAny<string>()) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(200, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}


		[Fact]
		public async Task StorageController_IsProductInWarehouse_Return_BadRequest()
		{

			var res = new ResponseDto("Product not in warehouse ");

			_mockStorageService.Setup(x => x.IsProductInWarehouse(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);

			//act
			var response = await controller.IsProductInWarehouse(It.IsAny<string>()) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.ToString().Should().Be(res.ToString());

		}


		//--StorageListByWarehouse tests---

		[Fact]
		public async Task StorageController_StorageListByWarehouse_ReturnOk()
		{
			//assert
			var storagesData = new StorageBuilder().Build();
			var storagesDtoData = new StorageDtoBuilder().Build();

			var res = new ResponseDto("Storage in warehouse ", storagesDtoData);

			_mockStorageService.Setup(x => x.StorageListByWarehouse(It.IsAny<string>())).ReturnsAsync(storagesData).Verifiable();
			_mockMapper.Setup(x => x.Map<List<StorageDto>>(storagesData)).Returns(storagesDtoData).Verifiable();

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);


			//act
			var response = await controller.StorageListByWarehouse(It.IsAny<string>()) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			Assert.Equal(200, response.StatusCode);
			response.Value.ToString().Should().Be(res.ToString());

		}


		[Fact]
		public async Task StorageController_StorageListByWarehouse_Return_BadRequest()
		{

			var res = new ResponseDto("Storage not in warehouse ");

			_mockStorageService.Setup(x => x.StorageListByWarehouse(It.IsAny<string>()));

			var controller = new StorageController(_mockStorageBaseService.Object, _mockMapper.Object, _mockStorageService.Object);

			//act
			var response = await controller.StorageListByWarehouse(It.IsAny<string>()) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.ToString().Should().Be(res.ToString());

		}



	}

	public class StorageBuilder
	{
		public List<Storage> Build()
		{
			List<Storage> list = new List<Storage>();
			var productData = new Product(Guid.NewGuid().ToString(), "cuaderno", "util", 10, Guid.NewGuid().ToString(), null, null);

			var warehouseData = new Warehouse(Guid.NewGuid().ToString(), "bodega", "calle", null);

			var storage = new Storage(Guid.NewGuid().ToString(), DateTime.Now, 10, productData.ProductId, productData, warehouseData.WarehouseId, warehouseData, null);
			list.Add(storage);
			return list;
		}
	}

	public class StorageDtoBuilder
	{
		public List<StorageDto> Build()
		{
			List<StorageDto> list = new List<StorageDto>();
			var productData = new ProductDto() { ProductId = Guid.NewGuid().ToString(), ProductName = "cuaderno", ProductDescription = "util", TotalQuantity = 10, Category = null };

			var warehouseData = new WarehouseDto() { WarehouseId = Guid.NewGuid().ToString(), WarehouseName = "bodega", WarehouseAddress = "calle", Storages = null };

			var storage = new StorageDto() { StorageId = Guid.NewGuid().ToString(), LastUpdate = DateTime.Now, PartialQuantity = 10, Product = productData, Warehouse = warehouseData, InputOutputs = null };

			list.Add(storage);
			return list;
		}
	}

}
