using AutoMapper;
using JRS.DR.WsModels;

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
        }
    }
}