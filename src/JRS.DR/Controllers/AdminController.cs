using System;
using System.Collections.Generic;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.Extensions;
using JRS.DR.ViewModels.Admin;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JRS.DR.Controllers
{
    public class AdminController : AppBaseController
    {
        private const int PageSize = 20;

        private readonly IApiService _apiService;
        private readonly IApplicationLogger _applicationLogger;

        public AdminController(IApiService apiService,
            IApplicationLogger applicationLogger,
            IConfigurationRoot configuration)
            : base(configuration)
        {
            _apiService = apiService;
            _applicationLogger = applicationLogger;
        }

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Auth(string password)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Index");

            Authenticate(password);

            if (!IsAuthenticated)
            {
                ViewBag.Error = "Parola incorecta!";
                return View("Auth");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            var model = new ObjectivesListModel();
            page = page ?? 1;

            model.Objectives = new List<ObjectiveLightModel>();
            model.Page = (int) page;

            //   var objectives = _objectivesRepository.GetAll();
            model.Objectives.Add(new ObjectiveLightModel { ObjectiveId = 1, Description = "test1", Name = "test1" });
            model.Objectives.Add(new ObjectiveLightModel { ObjectiveId = 2, Description = "test2", Name = "test2" });
            model.Objectives.Add(new ObjectiveLightModel { ObjectiveId = 3, Description = "test3", Name = "test3" });

            return View("Objectives", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            return View("CreateEdit", new ObjectiveModel());
        }

        [HttpPost]
        public IActionResult Create(ObjectiveModel model)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            try
            {
                var objective = Mapper.Map<ObjectiveRequest>(model);
                var request = new CreateObjectiveRequest
                {
                    Password = SessionPassword,
                    Objective = objective
                };

                var response = _apiService.CreateObjective(request);
                if (!response.IsError)
                {
                    ViewBag.Success = "Obiectivul a fost creat cu success!";
                    return RedirectToAction("Index");
                }

                response.Messages.ForEach(x => ModelState.AddModelError("", x));
                return View("CreateEdit", model);
            }
            catch (Exception exception)
            {
                _applicationLogger.LogError(exception);
                ViewBag.Error = GenericErrorMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int objectiveId)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            try
            {
                var getObjectiveRequest = new GetObjectiveRequest { ObjectiveId = objectiveId };
                var getObjectiveResponse = _apiService.GetObjective(getObjectiveRequest);

                var getObjectiveHtmlRequest = new GetObjectiveHtmlRequest { ObjectiveId = objectiveId };
                var getObjectiveHtmlResponse = _apiService.GetObjectiveHtml(getObjectiveHtmlRequest);

                if (!getObjectiveResponse.IsError && !getObjectiveHtmlResponse.IsError)
                {
                    var objectiveModel = Mapper.Map<ObjectiveModel>(getObjectiveResponse.Objective);
                    objectiveModel.Html = getObjectiveHtmlResponse.Html;
                    return View("CreateEdit", objectiveModel);
                }

                getObjectiveResponse.Messages.ForEach(x => ModelState.AddModelError("", x));
                getObjectiveResponse.Messages.ForEach(x => ModelState.AddModelError("", x));
                return View("CreateEdit", new ObjectiveModel());
            }
            catch (Exception exception)
            {
                _applicationLogger.LogError(exception);
                ViewBag.Error = GenericErrorMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ObjectiveModel model)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            try
            {
                var objective = Mapper.Map<ObjectiveRequest>(model);
                var request = new EditObjectiveRequest
                {
                    Password = SessionPassword,
                    Objective = objective
                };

                var response = _apiService.EditObjective(request);
                if (!response.IsError)
                {
                    ViewBag.Success = "Obiectivul a fost modificat cu success!";
                    return RedirectToAction("Index");
                }

                response.Messages.ForEach(x => ModelState.AddModelError("", x));
                return View("CreateEdit", model);
            }
            catch (Exception exception)
            {
                _applicationLogger.LogError(exception);
                ViewBag.Error = GenericErrorMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int objectiveId)
        {
            try
            {
                var request = new DeleteObjectiveRequest
                {
                    Password = SessionPassword,
                    ObjectiveId = objectiveId
                };
                var response = _apiService.DeleteObjective(request);
                return Json(response);
            }
            catch (Exception exception)
            {
                _applicationLogger.LogError(exception);
                return Json(new { IsError = true, Messages = new[] { exception.Message } });
            }
        }

        //ajax
        [HttpGet]
        public IActionResult GetLanguages()
        {
            var languagesResponse = _apiService.GetLanguages();
            var languagesModels = Mapper.Map<List<LanguageModel>>(languagesResponse.Languages);
            return Ok(languagesModels);
        }

        [HttpGet]
        public IActionResult GetObjectiveTypes(int? languageId)
        {
            var getObjectiveTypesRequest = new GetObjectiveTypesRequest { LanguageId = languageId };
            var getObjectiveTypesResponse = _apiService.GetObjectiveTypes(getObjectiveTypesRequest);
            var objectiveTypesModels = Mapper.Map<IEnumerable<ObjectiveTypeModel>>(getObjectiveTypesResponse.ObjectiveTypes);
            return Ok(objectiveTypesModels);
        }
    }
}