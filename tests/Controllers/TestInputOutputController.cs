using AutoMapper;
using ExerciceWebApi.Controllers;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using ExerciceWebApi.Models.Dtos.InputOutput;

namespace tests.Controllers
{
	public class TestInputOutputController
	{
		private Mock<IBaseCrudService<InputOutput>> _mockInputOutputService = new Mock<IBaseCrudService<InputOutput>>();

		private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();


		//--GetAll tests---

		[Fact]
		public async Task InputOutputController_GetInputOutput_ReturnOk()
		{

			var inputOutputsData = GetInputOutputsData();
			var inputOutputsDtoData = GetInputOutputsDtoData();

			var res = new ResponseDto("List of InputOutputs", inputOutputsDtoData);

			_mockInputOutputService.Setup(x => x.GetAll()).ReturnsAsync(inputOutputsData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<InputOutputDto>>(inputOutputsData)).Returns(inputOutputsDtoData).Verifiable();

			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.Get() as OkObjectResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			response.Should().NotBe(null);
			response.Value.ToString().Should().Be(res.ToString());

		}

		//[Fact]
		//public async Task InputOutputController_GetInputOutput_Catch_Exception()
		//{

		//	_mockInputOutputService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());

		//	var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.Get());


		//}


		[Fact]
		public async Task InputOutputController_GetInputOutput_Return_Not_content()
		{

			var inputOutputsData = new List<InputOutput>();
			var inputOutputsDtoData = new List<InputOutputDto>();


			_mockInputOutputService.Setup(x => x.GetAll()).ReturnsAsync(inputOutputsData).Verifiable();

			_mockMapper.Setup(x => x.Map<List<InputOutputDto>>(inputOutputsData)).Returns(inputOutputsDtoData).Verifiable();

			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.Get() as NoContentResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 204);

		}

		
		//--CreateInputOutput tests---

		[Fact]
		public async Task InputOutputController_CreateInputOutput_ReturnOk()
		{

			var inputOutputsData = GetInputOutputsData()[0];
			var inputOutputsDtoData = new CreateInputOutputDto() {Quantity=1,IsInput=true, StorageId="1"};

			var res = new ResponseDto("Input Output created" );

			_mockMapper.Setup(x => x.Map<InputOutput>(inputOutputsDtoData)).Returns(inputOutputsData).Verifiable();
			_mockInputOutputService.Setup(x => x.Create(inputOutputsData)).ReturnsAsync(inputOutputsData).Verifiable();


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.CreateInputOutput(inputOutputsDtoData) as CreatedResult;

			//assert
			response.Value.Should().BeEquivalentTo(res);
			response.Should().NotBe(null);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(201);
		}

		//[Fact]
		//public async Task InputOutputController_CreateInputOutput_Catch_Exception()
		//{
		//	var inputOutputsDtoData = new CreateInputOutputDto() { Quantity = 1, IsInput = true, StorageId = "1" };
		//	InputOutput inputOutputsData = null;

		//	_mockInputOutputService.Setup(x => x.Create(inputOutputsData)).ThrowsAsync(new Exception());

		//	var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.CreateInputOutput(inputOutputsDtoData));


		//}


		[Fact]
		public async Task InputOutputController_CreateInputOutput_Return_Not_created()
		{

			InputOutput inputOutputsData = null;

			var inputOutputsDtoData = new CreateInputOutputDto() { Quantity = 1, IsInput = true, StorageId = "1" };


			var res = new ResponseDto("Input or Output not create");

			_mockMapper.Setup(x => x.Map<InputOutput>(inputOutputsDtoData)).Returns(GetInputOutputsData()[0]).Verifiable();
			_mockInputOutputService.Setup(x => x.Create(inputOutputsData));


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.CreateInputOutput(inputOutputsDtoData) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.ToString().Should().Be(res.ToString());

		}


		//--UpdateInputOutput tests---

		[Fact]
		public async Task InputOutputController_UpdateInputOutput_ReturnOk()
		{

			var inputOutputsData = GetInputOutputsData()[0];
			var inputOutputsDtoData = new CreateInputOutputDto() { Quantity = 10, IsInput = true, StorageId = "1" };

			var res = new ResponseDto("Input or Output", GetInputOutputsDtoData()[0]);

			_mockMapper.Setup(x => x.Map<InputOutput>(inputOutputsDtoData)).Returns(inputOutputsData).Verifiable();
			_mockInputOutputService.Setup(x => x.Update("1",inputOutputsData)).ReturnsAsync(inputOutputsData).Verifiable();
			_mockMapper.Setup(x => x.Map<InputOutputDto>(inputOutputsData)).Returns(GetInputOutputsDtoData()[0]).Verifiable();


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateInputOutput("1",inputOutputsDtoData) as OkObjectResult;
			
			//assert
			response.Should().NotBe(null);
			response.Value.Should().BeEquivalentTo(res);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(200);
		}

		//[Fact]
		//public async Task InputOutputController_UpdateInputOutput_Catch_Exception()
		//{
		//	var inputOutputsDtoData = new CreateInputOutputDto() { Quantity = 10, IsInput = true, StorageId = "1" };
		//	InputOutput inputOutputsData = null;

		//	_mockMapper.Setup(x => x.Map<InputOutput>(inputOutputsDtoData)).Returns(inputOutputsData).Verifiable();
		//	_mockInputOutputService.Setup(x => x.Update("1",inputOutputsData)).ThrowsAsync(new Exception());

		//	var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.UpdateInputOutput("1",inputOutputsDtoData));


		//}


		[Fact]
		public async Task InputOutputController_UpdateInputOutput_Return_Not_updated()
		{

			InputOutput inputOutputsData = null;

			var inputOutputsDtoData = new CreateInputOutputDto() { Quantity = 1, IsInput = true, StorageId = "1" };


			var res = new ResponseDto("Input or Output not uptdated");

			_mockMapper.Setup(x => x.Map<InputOutput>(inputOutputsDtoData)).Returns(GetInputOutputsData()[0]).Verifiable();
			_mockInputOutputService.Setup(x => x.Update("1",inputOutputsData));


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.UpdateInputOutput("1",inputOutputsDtoData) as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.ToString().Should().Be(res.ToString());

		}



		//--DeleteInputOutput tests---

		[Fact]
		public async Task InputOutputController_DeleteInputOutput_ReturnOk()
		{

			var res = new ResponseDto("inputOutput  was deleted");

			_mockInputOutputService.Setup(x => x.Delete("1")).ReturnsAsync(true).Verifiable();


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.DeleteInputOutput("1") as OkObjectResult;

			//assert
			response.Should().NotBe(null);
			response.Value.Should().BeEquivalentTo(res);
			response.Value.ToString().Should().Be(res.ToString());
			response.StatusCode.Should().Be(200);
		}

		//[Fact]
		//public async Task InputOutputController_DeleteInputOutput_Catch_Exception()
		//{

		//	_mockInputOutputService.Setup(x => x.Delete("1")).ThrowsAsync(new Exception());

		//	var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);

		//	await Assert.ThrowsAsync<ServiceException>(async () => await controller.DeleteInputOutput("1"));


		//}


		[Fact]
		public async Task InputOutputController_DeleteInputOutput_Return_Not_updated()
		{


			var res = new ResponseDto("inputOutput not found");

			_mockInputOutputService.Setup(x => x.Delete("1"));


			var controller = new InputOutputController(_mockInputOutputService.Object, _mockMapper.Object);


			//act
			var response = await controller.DeleteInputOutput("1") as NotFoundObjectResult;

			//assert
			Assert.NotNull(response);
			Assert.True(response.StatusCode == 404);
			response.Value.ToString().Should().Be(res.ToString());

		}



		private List<InputOutput> GetInputOutputsData()
		{
			List<InputOutput> inputOutputsData = new List<InputOutput>{
				new InputOutput { InOutId ="1", InOutDate = DateTime.Parse("3-03-23"), IsInput=true, Quantity=10 ,StorageId ="1", Storage = null},
				new InputOutput { InOutId ="1", InOutDate = DateTime.Parse("2-03-23"), IsInput=false, Quantity=0 ,StorageId ="1", Storage = null},
				new InputOutput { InOutId ="1", InOutDate = DateTime.Parse("1-03-23"), IsInput=true, Quantity=5 ,StorageId ="1", Storage = null},
			};
			return inputOutputsData;
		}
		
		private List<InputOutputDto> GetInputOutputsDtoData()
		{
			List<InputOutputDto> inputOutputsData = new List<InputOutputDto>{
				new InputOutputDto { InOutId ="1", InOutDate = DateTime.Parse("3-03-23"), IsInput=true, Quantity=10 ,StorageId ="1", Storage = null},
				new InputOutputDto { InOutId ="1", InOutDate = DateTime.Parse("2-03-23"), IsInput=false, Quantity=0 ,StorageId ="1", Storage = null},
				new InputOutputDto { InOutId ="1", InOutDate = DateTime.Parse("1-03-23"), IsInput=true, Quantity=5 ,StorageId ="1", Storage = null},
			};
			return inputOutputsData;
		}
		

	}
}
