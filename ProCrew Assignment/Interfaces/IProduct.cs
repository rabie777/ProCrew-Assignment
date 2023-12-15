
using ProCrew_Assignment.Models;

namespace ProCrew_Assignment.Interfaces
{
    public interface IProduct
    {
        Task<IEnumerable<Product>>GetProducts();
        Task<Product> GetProductByIdAsync(int Id);
        Task<Product> CreateProduct(Product Obj);
        Task UpdateProductAsync(Product Obj);
        Task DeleteProductAsync(int id);



    }
}
