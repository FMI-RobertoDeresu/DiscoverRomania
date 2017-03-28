using JRS.DR.Exceptions;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JRS.DR.Helpers
{
    public static class CustomBadRequest
    {
        public static BadRequestObjectResult GetResult(ApiResponseBase response, ApiException exception)
        {
            response.IsError = true;

            foreach (var error in exception.Errors)
                response.Messages.Add(error);

            return new BadRequestObjectResult(response);
        }

        public static BadRequestObjectResult GetResult(ApiResponseBase response, ModelStateDictionary modelState)
        {
            response.IsError = !modelState.IsValid;

            foreach (var modelStateKey in modelState.Keys)
            foreach (var error in modelState[modelStateKey].Errors)
                response.Messages.Add(error.ErrorMessage);

            return new BadRequestObjectResult(response);
        }
    }
}