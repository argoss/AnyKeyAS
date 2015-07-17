using System;
using System.Linq;
using AutoMapper;
using Servicing.Account;
using Servicing.Clients;
using Servicing.Requests;
using Servicing.Users;
using Site.Models.Clients;
using Site.Models.Extensions;
using Site.Models.Requests;
using Site.Models.Service;
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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
            Mapper.CreateMap<RequestViewModel, RequestModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ToRequestStatus(src.Status)));

            Mapper.CreateMap<UserEditModel, UserEditViewModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.GetDisplayName()))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            Mapper.CreateMap<UserEditViewModel, UserEditModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => ToPosition(src.Position)))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Where(x => x.Flag).Select(x => x.Name)));

            Mapper.CreateMap<UserEditModel, UserViewModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.GetDisplayName()))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            Mapper.CreateMap<UserViewModel, UserEditModel>();

            Mapper.CreateMap<UserCreateModel, UserCreateViewModel>();
            Mapper.CreateMap<UserCreateViewModel, UserCreateModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => ToPosition(src.Position)))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Where(x => x.Flag).Select(x => x.Name)));

            Mapper.CreateMap<RequestModel, ServiceViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
            Mapper.CreateMap<ServiceViewModel, RequestModel>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => (RequestStatus)Enum.Parse(typeof(RequestStatus), src.Status, true)));
        }

        private Position ToPosition(string status)
        {
            switch (status)
            {
                case "Администратор":
                    return Position.Admin;
                case "Выездной инжинер":
                    return Position.ExitEngineer;
                case "Оператор":
                    return Position.Operator;
                case "Инжинер сервисного центра":
                    return Position.ServiceEngineer;
                default:
                    throw new Exception("Должность не существует!");
            };
        }

        private RequestStatus ToRequestStatus(string status)
        {
            switch (status)
            {
                case "Принята":
                    return RequestStatus.Accept;
                case "Доставка в СЦ":
                    return RequestStatus.DeliverySC;
                case "Доставлена в СЦ":
                    return RequestStatus.DeliveredSC;
                case "Обработана":
                    return RequestStatus.ProcessedSC;
                case "Доставка клиенту":
                    return RequestStatus.DeliveryC;
                case "Исполнена":
                    return RequestStatus.Performed;
                case "Возврат":
                    return RequestStatus.Return;
                default:
                    throw new Exception("Неизвестный статус!");
            };
        }
    }
}