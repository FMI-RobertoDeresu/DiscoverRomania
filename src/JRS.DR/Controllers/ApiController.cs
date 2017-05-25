using System;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.Exceptions;
using JRS.DR.Helpers;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace JRS.DR.Controllers
{
    public class ApiController : Controller
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IApplicationLogger _appLogger;
        private readonly IApiService _apiService;

        public ApiController(IConfigurationRoot configuration, IApplicationLogger appLogger, IApiService apiService)
        {
            _configuration = configuration;
            _appLogger = appLogger;
            _apiService = apiService;
        }

        [HttpPost("api/languages")]
        [Produces(typeof(GetLanguagesResponse))]
        public IActionResult GetLanguages()
        {
            return ApiCore<GetLanguagesResponse>(ModelState, output =>
            {
                var outputData = _apiService.GetLanguages();
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/objectiveTypes")]
        [Produces(typeof(GetObjectiveTypesResponse))]
        public IActionResult GetObjectiveTypes([FromBody] GetObjectiveTypesRequest request)
        {
            return ApiCore<GetObjectiveTypesRequest, GetObjectiveTypesResponse>(request, ModelState, (intput, output) =>
            {
                var outputData = _apiService.GetObjectiveTypes(intput);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/objective")]
        [Produces(typeof(GetObjectiveResponse))]
        public IActionResult GetObjective([FromBody] GetObjectiveRequest request)
        {
            return ApiCore<GetObjectiveRequest, GetObjectiveResponse>(request, ModelState, (input, output) =>
            {
                var outputData = _apiService.GetObjective(input);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/objectives")]
        [Produces(typeof(GetObjectivesResponse))]
        public IActionResult GetObjectives([FromBody] GetObjectivesRequest request)
        {
            return ApiCore<GetObjectivesRequest, GetObjectivesResponse>(request, ModelState, (input, output) =>
            {
                var outputData = _apiService.GetObjectives(input);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/objectiveHtml")]
        [Produces(typeof(GetObjectiveHtmlResponse))]
        public IActionResult GetObjectiveHtml([FromBody] GetObjectiveHtmlRequest request)
        {
            return ApiCore<GetObjectiveHtmlRequest, GetObjectiveHtmlResponse>(request, ModelState, (input, output) =>
            {
                var outputData = _apiService.GetObjectiveHtml(input);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/createObjective")]
        [Produces(typeof(CreateObjectiveResponse))]
        public IActionResult CreateObjective([FromBody] CreateObjectiveRequest request)
        {
            return ApiCore<CreateObjectiveRequest, CreateObjectiveResponse>(request, ModelState, (input, output) =>
            {
                if (input.Password != _configuration["password-manager"])
                    throw new ApiException("Incorrect password!");

                var outputData = _apiService.CreateObjective(input);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/editObjective")]
        [Produces(typeof(EditObjectiveResponse))]
        public IActionResult EditObjective([FromBody] EditObjectiveRequest request)
        {
            return ApiCore<EditObjectiveRequest, EditObjectiveResponse>(request, ModelState, (input, output) =>
            {
                if (input.Password != _configuration["password-manager"])
                    throw new ApiException("Incorrect password!");

                var outputData = _apiService.EditObjective(input);
                output = Mapper.Map(outputData, output);
            });
        }

        [HttpPost("api/deleteObjective")]
        [Produces(typeof(DeleteObjectiveResponse))]
        public IActionResult DeleteObjective([FromBody] DeleteObjectiveRequest request)
        {
            return ApiCore<DeleteObjectiveRequest, DeleteObjectiveResponse>(request, ModelState, (input, output) =>
            {
                if (input.Password != _configuration["password-manager"])
                    throw new ApiException("Incorrect password!");

                var outputData = _apiService.DeleteObjective(input);
                output = Mapper.Map(outputData, output);
            });
        }

        //utils
        private IActionResult ApiCore<TInput, TOutput>(TInput input, ModelStateDictionary modelState,
            Action<TInput, TOutput> coreAction)
            where TInput : new()
            where TOutput : ApiResponseBase, new()
        {
            var output = new TOutput();
            if (input == null)
                input = new TInput();

            try
            {
                if (!modelState.IsValid)
                    return CustomBadRequest.GetResult(output, modelState);

                coreAction(input, output);

                return Ok(output);
            }
            catch (ApiException apiException)
            {
                return CustomBadRequest.GetResult(output, apiException);
            }
            catch (Exception exception)
            {
                _appLogger.LogError(exception);
            }

            modelState.AddModelError("", _configuration["app:genericErrorMessage"]);
            return CustomBadRequest.GetResult(output, modelState);
        }

        private IActionResult ApiCore<TOutput>(ModelStateDictionary modelState, Action<TOutput> coreAction)
            where TOutput : ApiResponseBase, new()
        {
            var output = new TOutput();

            try
            {
                if (!modelState.IsValid)
                    return CustomBadRequest.GetResult(output, modelState);

                coreAction(output);

                return Ok(output);
            }
            catch (ApiException apiException)
            {
                return CustomBadRequest.GetResult(output, apiException);
            }
            catch (Exception exception)
            {
                _appLogger.LogError(exception);
            }

            modelState.AddModelError("", _configuration["app:genericErrorMessage"]);
            return CustomBadRequest.GetResult(output, modelState);
        }
    }
}