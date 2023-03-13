using AutoMapper;
using ExerciceWebApi.Controllers;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace tests.Controllers
{
	public class TestWarehoseController
	{
		private Mock<IBaseCrudService<Warehouse>> _mockWarehouseService = new Mock<IBaseCrudService<Warehouse>>();

		private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();


		//--GetAll tests---

		[Fact]
		public async Task Warehousecontroller_GetWarehouses_ReturnOk()
		{

			var warehouseData = GetWarehouseData();
			var warehouseDtoData = GetWarehouseDtoData();

			var res = new ResponseDto("List Warehouses", warehouseDtoData);

			_mockWarehouseService.Setup(x => x.GetAll()).ReturnsAsync(warehouseData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<WarehouseDto>>(warehouseData)).Returns(warehouseDtoData).Verifiable();

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.Get() as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(200);

		}

		//[Fact]
		//public async Task CategoryController_GetCategories_Catch_Exception()
		//{

		//	_mockWarehouseService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

		//	var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.Get());


		//}


		[Fact]
		public async Task CategoryController_GetCategories_Return_Not_content()
		{

			var warehouseData = new List<Warehouse>();
			var warehouseDtoData = new List<WarehouseDto>();

			_mockWarehouseService.Setup(x => x.GetAll()).ReturnsAsync(warehouseData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<WarehouseDto>>(warehouseData)).Returns(warehouseDtoData).Verifiable();

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.Get() as NoContentResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 204);

		}


		//--CreateWarehouse tests---

		[Fact]
		public async Task Warehousecontroller_CreateWarehouse_ReturnOk()
		{

			var warehouseData = GetWarehouseData()[0];
			var warehouseDtoData = new CreateWarehouseDto() { WarehouseName = "central", WarehouseAddress = "" };

			var res = new ResponseDto("Warehouse created");

			_mockMapper.Setup(x => x.Map<Warehouse>(warehouseDtoData)).Returns(warehouseData).Verifiable();
			_mockWarehouseService.Setup(x => x.Create(warehouseData)).ReturnsAsync(warehouseData).Verifiable();


			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.CreateWarehouse(warehouseDtoData) as CreatedResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(201);

		}

		//[Fact]
		//public async Task CategoryController_CreateWarehouse_Catch_Exception()
		//{
		//	Warehouse warehouseData = null;

		//	var warehouseDtoData = new CreateWarehouseDto() ;

		//	_mockWarehouseService.Setup(x => x.Create(warehouseData)).ThrowsAsync(new Exception());

		//	var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.CreateWarehouse(warehouseDtoData));


		//}


		[Fact]
		public async Task CategoryController_CreateWarehouse_Return_Not_content()
		{

			var warehouseData = GetWarehouseData()[0];
			var warehouseDtoData = new CreateWarehouseDto();

			var res = new ResponseDto("Warehouse not created");

			_mockMapper.Setup(x => x.Map<Warehouse>(warehouseDtoData)).Returns(warehouseData).Verifiable();
			_mockWarehouseService.Setup(x => x.Create(warehouseData));

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.CreateWarehouse(warehouseDtoData) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.Should().BeEquivalentTo(res);

		}


		//--UpdateWarehouse tests---

		[Fact]
		public async Task Warehousecontroller_UpdateWarehouse_ReturnOk()
		{

			var warehouseData = GetWarehouseData()[0];
			var newWarehouseDtoData = GetWarehouseDtoData()[0];
			var warehouseDtoData = new CreateWarehouseDto() { WarehouseName = "central", WarehouseAddress = "" };

			var res = new ResponseDto("Warehouse  was updated", newWarehouseDtoData);

			_mockMapper.Setup(x => x.Map<Warehouse>(warehouseDtoData)).Returns(warehouseData).Verifiable();
			_mockWarehouseService.Setup(x => x.Update("1", warehouseData)).ReturnsAsync(warehouseData).Verifiable();
			_mockMapper.Setup(x => x.Map<WarehouseDto>(warehouseData)).Returns(newWarehouseDtoData).Verifiable();

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateWarehouse("1",warehouseDtoData) as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(200);

		}

		//[Fact]
		//public async Task CategoryController_UpdateWarehouse_Catch_Exception()
		//{
		//	Warehouse warehouseData = null;

		//	var warehouseDtoData = new CreateWarehouseDto();

		//	_mockMapper.Setup(x => x.Map<Warehouse>(warehouseDtoData)).Returns(warehouseData).Verifiable();

		//	_mockWarehouseService.Setup(x => x.Update("1",warehouseData)).ThrowsAsync(new Exception());

		//	var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.UpdateWarehouse("1",warehouseDtoData));


		//}


		[Fact]
		public async Task CategoryController_UpdateWarehouse_Return_Not_content()
		{

			var warehouseData = GetWarehouseData()[0];
			var warehouseDtoData = new CreateWarehouseDto();

			var res = new ResponseDto("Warehouse not updated");

			_mockMapper.Setup(x => x.Map<Warehouse>(warehouseDtoData)).Returns(warehouseData).Verifiable();
			_mockWarehouseService.Setup(x => x.Update("1",warehouseData));

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateWarehouse("1",warehouseDtoData) as BadRequestObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 400);
			response.Value.Should().BeEquivalentTo(res);

		}



		//--DeleteWarehouse tests---

		[Fact]
		public async Task Warehousecontroller_DeleteWarehouse_ReturnOk()
		{


			var res = new ResponseDto("Warehouse  was deleted");

			_mockWarehouseService.Setup(x => x.Delete("1")).ReturnsAsync(true).Verifiable();

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.DeleteWarehouse("1") as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			Assert.NotNull(response);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(200);

		}

		//[Fact]
		//public async Task CategoryController_DeleteWarehouse_Catch_Exception()
		//{

		//	_mockWarehouseService.Setup(x => x.Delete("1")).ThrowsAsync(new Exception());

		//	var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.DeleteWarehouse("1"));


		//}


		[Fact]
		public async Task CategoryController_DeleteWarehouse_Return_Not_content()
		{

			
			var res = new ResponseDto("Warehouse not found");

			_mockWarehouseService.Setup(x => x.Delete("1"));

			var controller = new Warehousecontroller(_mockWarehouseService.Object, _mockMapper.Object);


			//act
			var response = await controller.DeleteWarehouse("1") as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.Should().BeEquivalentTo(res);

		}



		private List<Warehouse> GetWarehouseData()
		{
			List<Warehouse> warehousesData = new List<Warehouse>{
				new Warehouse { WarehouseId ="1", WarehouseName = "central ", WarehouseAddress ="", Storages = null },
				new Warehouse { WarehouseId ="2", WarehouseName = "south", WarehouseAddress ="", Storages = null },
			};
			return warehousesData;
		}

		private List<WarehouseDto> GetWarehouseDtoData()
		{
			List<WarehouseDto> warehousesData = new List<WarehouseDto>{
				new WarehouseDto { WarehouseId ="1", WarehouseName = "central ", WarehouseAddress ="", Storages = null },
				new WarehouseDto { WarehouseId ="2", WarehouseName = "south", WarehouseAddress ="", Storages = null },
			};
			return warehousesData;
		}

	}
}
