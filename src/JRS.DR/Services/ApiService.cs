using System;
using System.Collections.Generic;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.Exceptions;
using JRS.DR.Models.Common;
using JRS.DR.Models.Enums;
using JRS.DR.Validators;
using JRS.DR.WsModels;
using Microsoft.Extensions.Configuration;
using Language = JRS.DR.Models.Common.Language;
using Location = JRS.DR.Models.Common.Location;

namespace JRS.DR.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly IRepository<int, Language> _languageRepository;
        private readonly IRepository<int, ObjectiveType> _objectiveTypeRepository;

        public ApiService(
            IConfigurationRoot configuration,
            IObjectiveRepository objectiveRepository,
            IRepository<int, Language> languageRepository,
            IRepository<int, ObjectiveType> objectiveTypeRepository)
        {
            _configuration = configuration;
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

        public GetObjectivesResponse GetObjectives(GetObjectivesRequest input)
        {
            new ApiServiceValidator().ValidateGetObjectives(input);

            var objectives = _objectiveRepository.GetMany(x =>
                    x.Language.Id == input.LanguageId.Value &&
                    input.XLeftTop.Value <= x.Location.X && input.XRightBottom.Value >= x.Location.X &&
                    input.YLeftTop <= x.Location.Y && input.YRightBottom >= x.Location.Y,
                x => x.Type);

            foreach (var objective in objectives)
                objective.Type = _objectiveTypeRepository.Get(objective.ObjectiveTypeId);

            var response = new GetObjectivesResponse
            {
                Objectives = Mapper.Map<List<ObjectiveResponse>>(objectives)
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

        public CreateObjectiveResponse CreateObjective(CreateObjectiveRequest input)
        {
            if (input.Password != _configuration["password-manager"])
                throw new ApiException("Incorrect password!");

            new ApiServiceValidator().ValidateCreateObjective(input);

            var language = _languageRepository.Get(input.Objective.LanguageId.Value);

            if (language == null)
                throw new ApiException("Language does not exists!");

            var objectiveType = _objectiveTypeRepository.Get(input.Objective.ObjectiveTypeId.Value);

            if (objectiveType == null)
                throw new ApiException("Objective type does not exists!");

            if (objectiveType.Language != language)
                throw new ApiException("Objective type has other language than requested language!");

            var location = new Location(input.Objective.Location.X.Value, input.Objective.Location.Y.Value);
            var status = ObjectiveStatus.Accepted;

            var objective = new Objective(language, location, objectiveType, status,
                input.Objective.Name, input.Objective.Description, input.Objective.Picture, input.Objective.Html);

            location.PreInsert(); //TODO: temp way to fix CreatedAt
            _objectiveRepository.Create(objective);
            _objectiveRepository.SaveChanges();

            return new CreateObjectiveResponse
            {
                ObjectiveId = objective.Id
            };
        }

        public EditObjectiveResponse EditObjective(EditObjectiveRequest input)
        {
            if (input.Password != _configuration["password-manager"])
                throw new ApiException("Incorrect password!");

            new ApiServiceValidator().ValidateEditObjective(input);

            var language = _languageRepository.Get(input.Objective.LanguageId.Value);

            if (language == null)
                throw new ApiException("Language does not exists!");

            var objectiveType = _objectiveTypeRepository.Get(input.Objective.ObjectiveTypeId.Value);

            if (objectiveType == null)
                throw new ApiException("Objective type does not exists!");

            if (objectiveType.Language != language)
                throw new ApiException("Objective type has other language than requested language!");

            var objective = _objectiveRepository.Get(input.Objective.ObjectiveId.Value);
            if (objective == null)
                throw new ApiException("Objective does not exists!");

            objective.Language = language;
            objective.Type = objectiveType;
            objective.Name = input.Objective.Name;
            objective.Description = input.Objective.Description;
            objective.Picture = input.Objective.Picture;
            objective.Html = input.Objective.Html;
            objective.Location.X = input.Objective.Location.X.Value;
            objective.Location.Y = input.Objective.Location.Y.Value;

            _objectiveRepository.Update(objective);
            _objectiveRepository.SaveChanges();

            return new EditObjectiveResponse
            {
                ObjectiveId = objective.Id
            };
        }

        public DeleteObjectiveResponse DeleteObjective(DeleteObjectiveRequest input)
        {
            if (input.Password != _configuration["password-manager"])
                throw new ApiException("Incorrect password!");

            new ApiServiceValidator().ValidateDeleteObjective(input);

            var objective = _objectiveRepository.Get(input.ObjectiveId.Value);
            if (objective == null)
                throw new Exception("Objective does not exists!");

            _objectiveRepository.Delete(objective);
            _objectiveRepository.SaveChanges();

            return new DeleteObjectiveResponse();
        }
    }
}