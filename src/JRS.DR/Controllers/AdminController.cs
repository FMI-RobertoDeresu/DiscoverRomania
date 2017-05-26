using System;
using System.Collections.Generic;
using AutoMapper;
using JRS.DR.Contracts;
using JRS.DR.Extensions;
using JRS.DR.Models;
using JRS.DR.ViewModels.Admin;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JRS.DR.Controllers
{
    [Route("admin")]
    public class AdminController : AppBaseController
    {
        private readonly IApiService _apiService;
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly IApplicationLogger _applicationLogger;

        public AdminController(
            IApiService apiService,
            IObjectiveRepository objectiveRepository,
            IApplicationLogger applicationLogger,
            IConfigurationRoot configuration)
            : base(configuration)
        {
            _apiService = apiService;
            _objectiveRepository = objectiveRepository;
            _applicationLogger = applicationLogger;
        }

        [HttpGet("")]
        public IActionResult Index(int? page, int? persistentCount)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            page = page ?? 1;
            persistentCount = persistentCount ?? _objectiveRepository.CountAll();
            var objectives = _objectiveRepository.GetPaged(page.Value, PagedList.DefaultPageSize);

            var objectivesModels = Mapper.Map<IList<ObjectiveModel>>(objectives);
            var model = new PagedList<ObjectiveModel>(page.Value, persistentCount.Value, objectivesModels);

            return View(model);
        }

        [HttpGet("auth")]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost("auth")]
        public IActionResult Auth(string password)
        {
            if (IsAuthenticated)
                return RedirectToAction("Index");

            Authenticate(password);

            if (!IsAuthenticated)
            {
                ViewBag.Error = "Parola incorecta!";
                return View("Auth");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            return View("CreateEdit", new ObjectiveModel());
        }

        [HttpPost("create")]
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

        [HttpGet("edit")]
        public IActionResult Edit(int objectiveId)
        {
            if (!IsAuthenticated)
                return RedirectToAction("Auth");

            try
            {
                var objective = _objectiveRepository.Get(objectiveId);
                if (objective == null)
                {
                    ViewBag.Error = "Obiectivul nu a fost gasit.";
                    return RedirectToAction("Index");
                }

                var objectiveModel = Mapper.Map<ObjectiveModel>(objective);
                return View("CreateEdit", objectiveModel);
            }
            catch (Exception exception)
            {
                _applicationLogger.LogError(exception);
                ViewBag.Error = GenericErrorMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("edit")]
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

        [HttpDelete("delete")]
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
        [HttpGet("getLanguages")]
        public IActionResult GetLanguages()
        {
            var languagesResponse = _apiService.GetLanguages();
            var languagesModels = Mapper.Map<List<LanguageModel>>(languagesResponse.Languages);
            return Ok(languagesModels);
        }

        [HttpGet("getObjectiveTypes")]
        public IActionResult GetObjectiveTypes(int? languageId)
        {
            var getObjectiveTypesRequest = new GetObjectiveTypesRequest { LanguageId = languageId };
            var getObjectiveTypesResponse = _apiService.GetObjectiveTypes(getObjectiveTypesRequest);
            var objectiveTypesModels = Mapper.Map<IEnumerable<ObjectiveTypeModel>>(getObjectiveTypesResponse.ObjectiveTypes);
            return Ok(objectiveTypesModels);
        }
    }
}