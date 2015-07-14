using System;
using System.Linq;
using AutoMapper;
using Servicing.Account;
using Servicing.Clients;
using Servicing.Requests;
using Servicing.Users;
using Site.Models.Clients;
using Site.Models.Requests;
using Site.Models.Users;

namespace Site.Automapper
{
    public class ContentManagementProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ClientModel, ClientViewModel>();
            Mapper.CreateMap<ClientViewModel, ClientModel>();

            Mapper.CreateMap<RequestModel, RequestViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            Mapper.CreateMap<RequestViewModel, RequestModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(RequestStatus), src.Status)));

            Mapper.CreateMap<UserEditModel, UserEditViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            Mapper.CreateMap<UserEditViewModel, UserEditModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Where(x => x.Flag).Select(x => x.Name)));

            Mapper.CreateMap<UserEditModel, UserViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            Mapper.CreateMap<UserViewModel, UserEditModel>();

            Mapper.CreateMap<UserCreateModel, UserCreateViewModel>();
            Mapper.CreateMap<UserCreateViewModel, UserCreateModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Where(x => x.Flag).Select(x => x.Name)));

            /*Mapper.CreateMap<PageHistoryItemModel, PageHistoryItemViewModel>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PageId));
            Mapper.CreateMap<PageHistoryItemViewModel, PageHistoryItemModel>().
                ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.Id));*/
        }
    }
}