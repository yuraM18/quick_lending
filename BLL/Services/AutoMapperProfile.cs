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
            CreateMap<Employee, EmployeeDTO>()
                .ForMember("FirstName", opt => opt.MapFrom(p => p.People.FirstName))
                .ForMember("LastName", opt => opt.MapFrom(p => p.People.LastName));
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Person, UserDTO>();
        }
    }
}
