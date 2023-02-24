using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Position, PositionDto>();

            CreateMap<CountryDto, Country>();
            CreateMap<LocationDto, Location>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<PositionDto, Position>();
        }
    }
}