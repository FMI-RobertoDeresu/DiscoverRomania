using System.Collections.Generic;

namespace JRS.DR.WsModels
{
    public class GetObjectivesResponse : ApiResponseBase
    {
        public List<ObjectiveResponse> Objectives { get; set; }
    }
}