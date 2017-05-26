using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JRS.DR.Controllers
{
    public class AppBaseController : Controller
    {
        private const string IsSecureSessionKey = "IsSecureSession";
        private const string GenericError = "Ne pare rau, a aparut o eroare in timpul precesarii.";

        private readonly IConfigurationRoot _configuration;

        public AppBaseController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        protected string GenericErrorMessage => GenericError;

        protected bool IsAuthenticated => HttpContext.Session.GetString(IsSecureSessionKey) == "true";

        protected string SessionPassword => IsAuthenticated ? _configuration["password-manager"] : string.Empty;

        protected void Authenticate(string password)
        {
            if (_configuration["password-manager"] == password)
                HttpContext.Session.SetString(IsSecureSessionKey, "true");
        }
    }
}