using System.Collections.Generic;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.Models.Common;
using JRS.DR.Validators;
using JRS.DR.WsModels;

namespace JRS.DR.Services
{
    public class ApiService : IApiService
    {
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly IRepository<int, Language> _languageRepository;
        private readonly IRepository<int, ObjectiveType> _objectiveTypeRepository;

        public ApiService(
            IObjectiveRepository objectiveRepository,
            IRepository<int, Language> languageRepository,
            IRepository<int, ObjectiveType> objectiveTypeRepository)
        {
            _objectiveRepository = objectiveRepository;
            _languageRepository = languageRepository;
            _objectiveTypeRepository = objectiveTypeRepository;
        }

        public GetLanguagesResponse GetLanguages()
        {
            var languages = _languageRepository.GetAll();
            var response = new GetLanguagesResponse
            {
                Languages = Mapper.Map<List<LanguageResponse>>(languages)
            };

            return response;
        }

        public GetObjectiveTypesResponse GetObjectiveTypes(GetObjectiveTypesRequest input)
        {
            new ApiServiceValidator().ValidateGetObjectiveTypes(input);

            var objectiveTypes = _objectiveTypeRepository.GetMany(x => x.Language.Id == input.LanguageId.Value);
            var response = new GetObjectiveTypesResponse
            {
                ObjectiveTypes = Mapper.Map<IList<ObjectiveTypeResponse>>(objectiveTypes)
            };

            return response;
        }

        public GetObjectivesResponse GetObjectives(GetObjectivesRequest input)
        {
            new ApiServiceValidator().ValidateGetObjectives(input);

            var objectives = _objectiveRepository.GetMany(x =>
                x.Language.Id == input.LanguageId.Value &&
                input.XLeftTop.Value <= x.Location.X &&
                x.Location.X <= input.XRightBottom.Value &&
                input.YLeftTop <= x.Location.Y &&
                x.Location.Y <= input.YRightBottom);
            var response = new GetObjectivesResponse
            {
                Objectives = Mapper.Map<List<ObjectiveResponse>>(objectives)
            };

            return response;
        }

        public GetObjectiveResponse GetObjective(GetObjectiveRequest input)
        {
            new ApiServiceValidator().ValidateGetObjective(input);

            var objective = _objectiveRepository.Get(input.ObjectiveId.Value);
            var response = new GetObjectiveResponse
            {
                Objective = Mapper.Map<ObjectiveResponse>(objective)
            };

            return response;
        }

        public GetObjectiveHtmlResponse GetObjectiveHtml(GetObjectiveHtmlRequest input)
        {
            new ApiServiceValidator().ValidateGetObjectiveHtml(input);

            var objective = _objectiveRepository.Get(input.ObjectiveId.Value);
            var response = Mapper.Map<GetObjectiveHtmlResponse>(objective);

            return response;
        }
    }
}