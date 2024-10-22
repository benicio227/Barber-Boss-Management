using AutoMapper;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Application.AutoMapper;
public class AutoMapping : Profile
// AutoMapping é uma classe pública que herda de Profile, é uma classe base do AutoMapper usada para definir
// configurações de mapeamento 
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRevenueJson, Revenue>();
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        CreateMap<RequestBarberShopJson, BarberShop>();
        CreateMap<RequestBarberShopJson, BarberShop>();

    }

    private void EntityToResponse()
    {
        CreateMap<Revenue, ResponseRegisteredRevenueJson>();
            
        CreateMap<Revenue, ResponseShortRevenueJson>();

        CreateMap<Revenue, ResponseRevenueJson>()
            .ForMember(dest => dest.BarberShop, opt => opt.MapFrom(src => new ResponseBarberShopJson
            {
                Id = src.BarberShop.Id,
                Name = src.BarberShop.Name,
                Address = src.BarberShop.Address,
                PhoneNumber = src.BarberShop.PhoneNumber,
                UserId = src.BarberShop.UserId
            }));
      
        CreateMap<User, ResponseUserProfileJson>();

        CreateMap<BarberShop, ResponseBarberShopJson>()
            .ForMember(dest => dest.Revenues, opt => opt.MapFrom(src => src.Revenues));
        //


        CreateMap<BarberShop, ResponseShortBarberJson>();
     
    }
}
