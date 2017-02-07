using AutoMapper;
using WebTelNET.Office.Libs.Models;
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

            #endregion

        }
    }
}