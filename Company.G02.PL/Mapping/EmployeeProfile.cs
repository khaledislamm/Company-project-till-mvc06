using AutoMapper;
using Company.G02.DAL.Models;
using Company.G02.PL.Dto;

namespace Company.G02.PL.Mapping
{
    public class EmployeeProfile : Profile
    {

        // CLR
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
               /* .ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName))*/;
            CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
