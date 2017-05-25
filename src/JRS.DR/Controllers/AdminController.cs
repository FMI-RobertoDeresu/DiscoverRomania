using System;
using System.Collections.Generic;
using JRS.DR.Contracts;
using JRS.DR.ViewModels.Admin;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JRS.DR.Controllers
{
    public class AdminController : Controller
    {
        private const int PageSize = 20;

        private readonly IApiService _apiService;
        private readonly IApplicationLogger _applicationLogger;
        private readonly IConfigurationRoot _configuration;

        public AdminController(IApiService apiService, IApplicationLogger applicationLogger, IConfigurationRoot configuration)
        {
            _apiService = apiService;
            _applicationLogger = applicationLogger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Auth(string password)
        {
            if (_configuration["password-manager"] == password)
            {
                //session
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("","");
            return View("Auth");
        }

        public IActionResult Index(int? page)
        {
            page = page ?? 1;
            return View("Index", new List<ObjectiveLightModel> { new ObjectiveLightModel() });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateEdit", new ObjectiveModel());
        }

        [HttpPost]
        public IActionResult Create(ObjectiveModel editModel)
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View("CreateEdit", new ObjectiveModel());
        }

        [HttpPost]
        public IActionResult Edit(ObjectiveModel editModel)
        {
            return RedirectToAction("Edit");
        }

        [HttpDelete]
        public IActionResult Delete(int objectiveId)
        {
            try
            {
                var request = new DeleteObjectiveRequest
                {
                    Password = "",
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
    }
}