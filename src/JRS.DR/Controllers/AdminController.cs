using System;
using System.Collections.Generic;
using JRS.DR.Contracts;
using JRS.DR.ViewModels.Admin;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using JRS.DR.Models.Common;

namespace JRS.DR.Controllers
{
    public class AdminController : Controller
    {
        private const int PageSize = 20;

        private readonly IApiService _apiService;
        private readonly IApplicationLogger _applicationLogger;
        private readonly IConfigurationRoot _configuration;
        private readonly IRepository<int, Objective> _objectivesRepository;

        public AdminController(IApiService apiService, IApplicationLogger applicationLogger, IConfigurationRoot configuration, IRepository<int, Objective> objectivesRepository)
        {
            _apiService = apiService;
            _applicationLogger = applicationLogger;
            _configuration = configuration;
            _objectivesRepository = objectivesRepository;
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
                HttpContext.Session.SetString("password", password);
                HttpContext.Session.SetString("isLoggedIn", "true");
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Parola incorecta!";
            return View("Auth");
        }

        public IActionResult Index(int? page)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null)
            {
                return RedirectToAction("Auth");
            }

            var model = new ObjectivesListModel();
            page = page ?? 1;

            model.Objectives = new List<ObjectiveLightModel>();
            model.Page = (int)page;

            //   var objectives = _objectivesRepository.GetAll();
            model.Objectives.Add(new ObjectiveLightModel() { ObjectiveId = 1, Description = "test1", Name = "test1"  });
            model.Objectives.Add(new ObjectiveLightModel() { ObjectiveId = 2, Description = "test2", Name = "test2" });
            model.Objectives.Add(new ObjectiveLightModel() { ObjectiveId = 3, Description = "test3", Name = "test3" });

            ViewBag.LoggedIn = HttpContext.Session.GetString("isLoggedIn");
            return View("Objectives", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null)
            {
                return RedirectToAction("Auth");
            }

            var model = new ObjectiveModel();
            model.Languages = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            model.ObjectiveTypes = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            ViewBag.LoggedIn = HttpContext.Session.GetString("isLoggedIn");
            return View("CreateEdit", model);
        }

        [HttpPost]
        public IActionResult Create(ObjectiveModel editModel)
        {
            if (editModel.Location.X == null || editModel.Location.Y == null)
            {
                ModelState.AddModelError("Location", "Va rugam sa alegeti locatia");
            }

            if (!ModelState.IsValid)
            {
                return View("CreateEdit", editModel);
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult Edit(int objectiveId)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null)
            {
                return RedirectToAction("Auth");
            }

            ViewBag.LoggedIn = HttpContext.Session.GetString("isLoggedIn");
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
                    Password = HttpContext.Session.GetString("password") ?? "",
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