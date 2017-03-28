using System.Collections.Generic;

namespace JRS.DR.WsModels
{
    public class GetLanguagesResponse : ApiResponseBase
    {
        public List<LanguageResponse> Languages { get; set; }
    }
}