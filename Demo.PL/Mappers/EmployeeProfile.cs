using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;

namespace Demo.PL.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.Name));

            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}
