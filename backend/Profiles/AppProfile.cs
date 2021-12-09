
using AutoMapper;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;

namespace ISO810_ERP.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Organization, OrganizationDto>().ReverseMap();
        }
    }
}