using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProCrew_Assignment.DTO;
using ProCrew_Assignment.Helper;
using ProCrew_Assignment.Interfaces;
using ProCrew_Assignment.Models;
using ProCrew_Assignment.Services;
using System.Text.Json;

namespace ProCrew_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _Product;
        private readonly AuditService _auditService;
        private readonly IMapper _mapper;

        public ProductsController(IProduct product,AuditService auditService, IMapper mapper)
        {
            _Product = product;
            _auditService = auditService;
            _mapper = mapper;
        }


        #region GetProducts

        [HttpGet]
        [Route("~/api/controller/GetProducts")]
        public async Task<IActionResult> AllProducts()
        {
            try
            {
                var products = await _Product.GetProducts();
                return Ok(new APIResponses<IEnumerable<Product>>()
                {
                    Code = 200,
                    status = "Ok",
                    Message = "Data Found",
                    Data = products
                });
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponses<string>()
                {
                    Code = 404,
                    status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }

        }
        #endregion 

        #region GetById
        [HttpGet]
        [Route("api/[controller]/GetProductById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _Product.GetProductByIdAsync(id);

                return Ok(new APIResponses<Product>()
                {
                    Code = 200,
                    status = "Ok",
                    Message = "Data Found",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponses<string>()
                {
                    Code = 404,
                    status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }
        }

        #endregion

        #region Create
        [HttpPost]
        [Route("~/api/[controller]/CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto obj)
        {
             
            try
            {

                if (ModelState.IsValid)
                {

                    var model = _mapper.Map<Product>(obj);
                    var result = await _Product.CreateProduct(model);
                    _auditService.LogAuditAsync("Products", model.Id, "CREATE", null, JsonSerializer.Serialize(model));

                    return Ok(new APIResponses<Product>()
                    {
                        Code = 200,
                        status = "Ok",
                        Message = "Data product added successfully",
                        Data = result
                    });
                }

                return BadRequest(new APIResponses<string>()
                {
                    Code = 400,
                    status = "Bad Request",
                    Message = "Validation Error"
                });


            }
            catch (Exception ex)
            {
                return NotFound(new APIResponses<string>()
                {
                    Code = 404,
                    status = "Not Found",
                    Message = "Data Not Added",
                    Data = ex.Message
                }); 
            }
             
        }
        #endregion

        #region edit
        [HttpPut]
        [Route("api/[controller]/UpdateProduct")]
        public async Task<IActionResult> Update(Product obj)
        {
            try
            {
                await _Product.UpdateProductAsync(obj); 
                return Ok(new APIResponses<string>()
                {

                    Code = 201,
                    status = "Updated",
                    Message = "Data Updated",
                    Data = "No result to return"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponses<string>()
                {

                    Code = 400,
                    status = "Bad Request",
                    Message = "Data Not Updated",
                    Data = ex.Message

                });
            }
        }
            #endregion

        #region delete
            [HttpDelete]
        [Route("/api/[controller]/delete/{id}")]
        public async Task< IActionResult >Delete(int id)
        {
            try
            {
                await _Product.DeleteProductAsync(id);
                return Ok(new APIResponses<string>()
                {
                    Code = 201,
                    status = "Deleted",
                    Message = "Data Deleted",
                    Data = "No result to return"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponses<string>()
                { 
                    Code = 400,
                    status = "Bad Request",
                    Message = "Data Not Deleted",
                    Data = ex.Message 
                });
            }
        }
        #endregion

    }
}
