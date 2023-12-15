using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProCrew_Assignment.Data;
using ProCrew_Assignment.Interfaces;
using ProCrew_Assignment.Models;

namespace ProCrew_Assignment.Repository
{
    public class ProductRep : IProduct
    {
        private readonly ApplicationDbContext _DB;
        public ProductRep(ApplicationDbContext dB)
        {
            _DB = dB;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _DB.Products.AsNoTracking().ToListAsync(); 
        }
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var data=  await _DB.Products.FindAsync(Id);
            if(data is null)
            {
                throw new ArgumentException();
            }
            return await _DB.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<Product> CreateProduct(Product Obj)
        {
            await _DB.Products.AddAsync(Obj);
            await _DB.SaveChangesAsync();
            var result =await _DB.Products.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task DeleteProductAsync(int id)
        {
            var olddata = await _DB.Products.FindAsync(id);
            if (olddata is null)
                throw new ArgumentException();
            _DB.Products.Remove(olddata);
            await _DB.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product Obj)
        {
            var olddata = await _DB.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Obj.Id); 
            _DB.Entry(Obj).State = EntityState.Modified;
            await _DB.SaveChangesAsync();
        }
    }
}
