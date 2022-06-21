using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StatementTypeDTO, StatementType>().ReverseMap();
            CreateMap<PersonDTO, Person>().ReverseMap();
            CreateMap<EmployeeDTO, Employee>().ReverseMap();

        }
    }
}
