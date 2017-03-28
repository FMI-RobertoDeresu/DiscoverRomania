using System.Collections.Generic;

namespace JRS.DR.WsModels
{
    public class GetObjectiveTypesResponse : ApiResponseBase
    {
        public IList<ObjectiveTypeResponse> ObjectiveTypes { get; set; }
    }
}