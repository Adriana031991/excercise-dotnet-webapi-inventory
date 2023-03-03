
using AutoMapper;
using ExerciceWebApi.Controllers;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.Response;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services.Gateway;
using ExerciceWebApi.Utilities.ServiceException;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace tests.Controllers;

public class TestProductController
{
    private readonly IBaseCrudService<Product> _productService;
    private Mock<IBaseCrudService<Product>> _mockProductService;
    private readonly IMapper _mapper;

    public TestProductController()
    {
        _productService = A.Fake<IBaseCrudService<Product>>();
        _mapper = A.Fake<IMapper>();

    }

    private List<ProductDto> GetProductsDtoData()
    {
        List<ProductDto> productsDtoData = new List<ProductDto>
        {
            new ProductDto
            {
                ProductId = "1",
                ProductName = "IPhone",
                ProductDescription = "IPhone 12",
                TotalQuantity = 10,
                Category = null

            },
             new ProductDto
            {
                ProductId = "2",
                ProductName = "Laptop",
                ProductDescription = "HP Pavilion",
                CategoryId = "12",
                TotalQuantity = 20,
                Category = null

            },
             new ProductDto
            {
                ProductId = "3",
                ProductName = "TV",
                ProductDescription = "Samsung Smart TV",
                CategoryId = "12",
                TotalQuantity = 30,
                Category = null
            },
        };


        return productsDtoData;

    }

    private List<Product> GetProductsData()
    {
        List<Product> productsData = new List<Product>
        {
            new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            ),
            new Product(

                "1",
                "laptop",
                "pc 12",
                10,
                "12",
                null,
                null

            ),
            new Product(

                "1",
                "celphone",
                "cell 12",
                10,
                "12",
                null,
                null

            ),

        };
        return productsData;
    }

    [Fact]
    public async void ProductController_GetProducts_Return_No_Content()
    {
        //Arrange
        var products = A.Fake<ICollection<ProductDto>>();
        var productList = A.Fake<List<ProductDto>>();
        A.CallTo(() => _mapper.Map<List<ProductDto>>(products)).Returns(productList);
        var controller = new ProductController(_productService, _mapper);

        //Act
        var result = await controller.Get();

        //Assert
        result.Should().BeOfType(typeof(NoContentResult));

    }

    [Fact]
    public async void ProductController_GetProducts_ReturnOk()
    {
        //Arrange

        var productData = GetProductsData();
        var productDtoData = GetProductsDtoData();

        A.CallTo(() => _productService.GetAll()).Returns(productData);
        A.CallTo(() => _mapper.Map<List<ProductDto>>(productData)).Returns(productDtoData);
        var controller = new ProductController(_productService, _mapper);

        //Act
        var result = await controller.Get();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        //result.Result.Should().NotBeNull();
        //result.Result.Should().BeOfType(typeof(OkObjectResult));
        //Assert.IsType<OkObjectResult>(result.Result);
    }


    [Fact]
    public async Task ProductController_GetProducts_Result_Be_Same_type()
    {
        //Arrange
        var productData = GetProductsData();
        var productDtoData = GetProductsDtoData();

        A.CallTo(() => _productService.GetAll()).Returns(productData);
        A.CallTo(() => _mapper.Map<List<ProductDto>>(productData)).Returns(productDtoData);
        var controller = new ProductController(_productService, _mapper);
        var res = new ResponseDto("List products", productDtoData);

        //Act
        var response = await controller.Get() as OkObjectResult;

        //Assert
        Assert.True(res.GetType() == response.Value.GetType());
    }



    [Fact]
    public async Task ProductController_GetProducts_Result_Have_lenght_more_than_zero()
    {
        //Arrange
        _mockProductService = new Mock<IBaseCrudService<Product>>();
        var _mockMapper = new Mock<IMapper>();

        var productData = GetProductsData();
        var productDtoData = GetProductsDtoData();

        var res = new ResponseDto("List products", productDtoData);

        _mockProductService.Setup(x => x.GetAll()).ReturnsAsync(productData).Verifiable();

        _mockMapper.Setup(x => x.Map<List<ProductDto>>(productData)).Returns(productDtoData).Verifiable();

        var controller = new ProductController(_mockProductService.Object, _mockMapper.Object);


        //act
        var response = await controller.Get() as OkObjectResult;

        //assert
        response.Value.Should().BeEquivalentTo(res);//pasa
        Assert.NotNull(response);//pasa
		response.Value.ToString().Should().Be(res.ToString());//pasa
		//r.Value.Should().Be(res);//falla


    }


    [Fact]
    public async Task ProductController_CreateProduct_Result_Created()
    {
        //Arrange
        var productData = new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );

        var productDtoData = new CreateProductDto
        {
            ProductName = "IPhone",
            ProductDescription = "IPhone 12",
            TotalQuantity = 10,
        };

        A.CallTo(() => _mapper.Map<Product>(productDtoData)).Returns(productData);
        A.CallTo(() => _productService.Create(productData)).Returns(Task.FromResult(productData));
        var controller = new ProductController(_productService, _mapper);
        var res = new ResponseDto("Product created");

        //Act
        var response = await controller.CreateProduct(productDtoData) as CreatedResult;

        //Assert
        response.StatusCode.Should().Be(201);
        response.Value.Should().BeEquivalentTo(res);
    }


    [Fact]
    public async Task ProductController_CreateProduct_Result_Catch_Exception()
    {

        var productData = A.Fake<Product>();
        var productDtoData = A.Fake<CreateProductDto>();

        A.CallTo(() => _productService.Create(productData)).ThrowsAsync(new Exception());
        var controller = new ProductController(_productService, _mapper);


        await Assert.ThrowsAsync<Exception>(() => _productService.Create(productData));

	}

	[Fact]
    public async Task ProductController_CreateProduct_Result_Not_Created()
    {
        //Arrange
        var productData = new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );
        Product productNull = null;
        var productDtoData = new CreateProductDto
        {
            ProductName = "IPhone",
            ProductDescription = "IPhone 12",
            TotalQuantity = 10,
        };

        var res = new ResponseDto("Product not created");

        A.CallTo(() => _mapper.Map<Product>(productDtoData)).Returns(productData);
        A.CallTo(() => _productService.Create(productData)).Returns(productNull);
        var controller = new ProductController(_productService, _mapper);

        //Act

        var response = await controller.CreateProduct(productDtoData) as BadRequestObjectResult;

        //Assert

        response.Should().NotBeNull();
        Assert.Equal(400, response.StatusCode);
        Assert.Equal(res.ToString(), response.Value.ToString());



    }


    //----

    [Fact]
    public async Task ProductController_UpdateProduct_Result_Updated()
    {
        //Arrange
        var productData = new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );

        var newProductData = new Product(

                "1",
                "computer",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );

        var productDtoData = new CreateProductDto
        {
            ProductName = "computer",
            ProductDescription = "IPhone 12",
            TotalQuantity = 10,
        };

        A.CallTo(() => _mapper.Map<Product>(productDtoData)).Returns(productData);
        A.CallTo(() => _productService.Update("1", productData)).Returns(Task.FromResult(newProductData));
        var controller = new ProductController(_productService, _mapper);
        var res = new ResponseDto("product  was updated", newProductData);

        //Act
        var response = await controller.UpdateProduct("1", productDtoData) as OkObjectResult;

        //Assert
        response.StatusCode.Should().Be(200);
        Assert.Equal(res.ToString(), response.Value.ToString());
        Assert.Equal(res.GetType(), response.Value.GetType());
    }


    [Fact]
    public async Task ProductController_UpdateProduct_Result_Catch_Exception()
    {
        //Arrange
        var productData = A.Fake<Product>();
        var productDtoData = A.Fake<CreateProductDto>();

        A.CallTo(() => _productService.Update("1", productData)).ThrowsAsync(new Exception());
        var controller = new ProductController(_productService, _mapper);

        await Assert.ThrowsAsync<Exception>(() => _productService.Update("1",productData));

    }

    [Fact]
    public async Task ProductController_UpdateProduct_Result_Not_Updated()
    {
        //Arrange
        var productData = new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );
        Product productNull = null;
        var productDtoData = new CreateProductDto
        {
            ProductName = "IPhone",
            ProductDescription = "IPhone 12",
            TotalQuantity = 10,
        };

        var res = new ResponseDto("Product not found");

        A.CallTo(() => _mapper.Map<Product>(productDtoData)).Returns(productData);
        A.CallTo(() => _productService.Update("1", productData)).Returns(productNull);
        var controller = new ProductController(_productService, _mapper);

        //Act

        var response = await controller.UpdateProduct("1", productDtoData) as NotFoundObjectResult;

        //Assert

        response.Should().NotBeNull();
        Assert.Equal(404, response.StatusCode);
        Assert.Equal(res.ToString(), response.Value.ToString());



    }

    //----

    [Fact]
    public async Task ProductController_DeleteProduct_Result_Ok()
    {
        //Arrange
        A.CallTo(() => _productService.Delete("1")).Returns(Task.FromResult(true));
        var controller = new ProductController(_productService, _mapper);
        var res = new ResponseDto("product  was deleted");

        //Act
        var response = await controller.DeleteProduct("1") as OkObjectResult;

        //Assert
        response.StatusCode.Should().Be(200);
        Assert.Equal(res.ToString(), response.Value.ToString());
    }


    [Fact]
    public async Task ProductController_DeleteProduct_Result_Catch_Exception()
    {


        A.CallTo(() => _productService.Delete("1")).ThrowsAsync(new Exception());
        var controller = new ProductController(_productService, _mapper);

        await Assert.ThrowsAsync<Exception>(() => _productService.Delete("1"));

    }

    [Fact]
    public async Task ProductController_DeleteProduct_Result_Not_Delete()
    {
        //Arrange
        var productData = new Product(

                "1",
                "IPhone",
                "IPhone 12",
                10,
                "12",
                null,
                null

            );
        Product productNull = null;
        var productDtoData = new CreateProductDto
        {
            ProductName = "IPhone",
            ProductDescription = "IPhone 12",
            TotalQuantity = 10,
        };

        var res = new ResponseDto("Product not found");

        A.CallTo(() => _mapper.Map<Product>(productDtoData)).Returns(productData);
        A.CallTo(() => _productService.Update("1", productData)).Returns(productNull);
        var controller = new ProductController(_productService, _mapper);

        //Act

        var response = await controller.UpdateProduct("1", productDtoData) as NotFoundObjectResult;

        //Assert

        response.Should().NotBeNull();
        Assert.Equal(404, response.StatusCode);
        Assert.Equal(res.ToString(), response.Value.ToString());



    }
}