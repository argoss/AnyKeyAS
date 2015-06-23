﻿using AutoMapper;
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

            Mapper.CreateMap<RequestModel, RequestViewModel>();
            Mapper.CreateMap<RequestViewModel, RequestModel>();

            Mapper.CreateMap<UserEditModel, UserViewModel>();
            Mapper.CreateMap<UserViewModel, UserEditModel>();

            /*Mapper.CreateMap<PageHistoryItemModel, PageHistoryItemViewModel>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PageId));
            Mapper.CreateMap<PageHistoryItemViewModel, PageHistoryItemModel>().
                ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.Id));*/
        }
    }
}