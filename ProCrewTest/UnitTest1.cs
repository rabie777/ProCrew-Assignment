using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ProCrew_Assignment.Controllers;
using ProCrew_Assignment.DTO;
using ProCrew_Assignment.Helper;
using ProCrew_Assignment.Interfaces;
using ProCrew_Assignment.Models;
using ProCrew_Assignment.Services;

namespace ProCrewTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Getproduct_allDataValid_returnSuccesswithResponse()
        {
            // Arrange
            var productService = A.Fake<IProduct>();
            var auditService = A.Fake<AuditService>();
            var mapper = A.Fake<IMapper>();

            var controller = new ProductsController(productService, auditService, mapper);

            var productDto = new ProductDto
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.99m,
                Quantity = 5
            };

            // Act
            var result = await controller.CreateProduct(productDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<APIResponses<Product>>(okResult.Value);

            Assert.Equal(200, apiResponse.Code);
            Assert.Equal("Ok", apiResponse.status);
            Assert.Equal("Data product added successfully", apiResponse.Message);

        }
    }
}

