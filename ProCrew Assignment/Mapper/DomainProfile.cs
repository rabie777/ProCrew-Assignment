using AutoMapper;
using ProCrew_Assignment.DTO;
using ProCrew_Assignment.Models;

namespace ProCrew_Assignment.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

        }
    }
}
