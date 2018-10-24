using System;
using AutoMapper;
using JRS.DR.Models.Common;
using JRS.DR.ViewModels.Admin;
using JRS.DR.WsModels;
using Location = JRS.DR.WsModels.Location;

namespace JRS.DR.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //self mapping to avoid assignable mapper
            CreateMap<GetLanguagesResponse, GetLanguagesResponse>();
            CreateMap<GetObjectiveTypesResponse, GetObjectiveTypesResponse>();
            CreateMap<GetObjectivesResponse, GetObjectivesResponse>();
            CreateMap<GetObjectiveResponse, GetObjectiveResponse>();
            CreateMap<GetObjectiveHtmlResponse, GetObjectiveHtmlResponse>();
            CreateMap<CreateObjectiveResponse, CreateObjectiveResponse>();
            CreateMap<EditObjectiveResponse, EditObjectiveResponse>();
            CreateMap<DeleteObjectiveResponse, DeleteObjectiveResponse>();

            //view models -> service models
            CreateMap<ObjectiveModel, ObjectiveRequest>();
            CreateMap<LocationModel, Location>();

            //service models -> view models
            CreateMap<Language, LanguageModel>();
            CreateMap<ObjectiveType, ObjectiveTypeModel>();

            //domain models -> view models
            CreateMap<Objective, GetObjectiveHtmlResponse>()
                .ForMember(dest => dest.IsError, map => map.Ignore())
                .ForMember(dest => dest.Messages, map => map.Ignore());
            CreateMap<ObjectiveLight, ObjectiveModel>()
                .ForMember(dest => dest.ObjectiveId, map => map.MapFrom(src => src.Id));
            CreateMap<Objective, ObjectiveModel>()
                .ForMember(dest => dest.ObjectiveId, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.LanguageId, map => map.MapFrom(src => src.Language.Id))
                .ForMember(dest => dest.ObjectiveTypeId, map => map.MapFrom(src => src.Type.Id));
        }
    }
}