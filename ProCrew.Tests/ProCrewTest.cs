

using Microsoft.AspNetCore.Mvc;
using ProCrew_Assignment.Controllers;
using ProCrew_Assignment.Models;

namespace ProCrew.Tests
{
    
    public class ProCrewTest
    {
        [Fact] 
        public void Getproduct_allDataValid_returnSuccesswithResponse()
        {
            //Arrange
            var controller = new ProductsController();
            //Act
            var result = controller.Get(2);
            //Assert
            Assert.IsType<Product>(result);
        }

    }
}
