using System.Linq;
using AutoMapper;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Libs
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            #region Maps_for_response_models

            CreateMap<UserService, ServiceInfoResponseModel>()
                .ForMember(m => m.ActivationDateTime, o => o.MapFrom(s => s.ActivationDateTime))
                .ForMember(m => m.ServiceType, o => o.MapFrom(s => s.ServiceProvider.ServiceType.Name))
                .ForMember(m => m.Status, o => o.MapFrom(s => s.ServiceStatus.Id));

            CreateMap<ServiceProvider, ServiceProviderResponseModel>()
                .ForMember(m => m.Name, o => o.MapFrom(s => s.Name))
                .ForMember(m => m.Description, o => o.MapFrom(s => s.Description))
                .ForMember(m => m.WebSite, o => o.MapFrom(s => s.WebSite))
                .ForMember(m => m.ServiceTypeId, o => o.MapFrom(s => s.ServiceTypeId));

            CreateMap<UserService, UserServiceResponseModel>()
                .ForMember(m => m.ActivationDateTime, o => o.MapFrom(s => s.ActivationDateTime))
                .ForMember(m => m.ServiceType, o => o.MapFrom(s => s.ServiceProvider.ServiceType.Name))
                .ForMember(m => m.Status, o => o.MapFrom(s => s.ServiceStatus.Id));

            #endregion

        }
    }
}