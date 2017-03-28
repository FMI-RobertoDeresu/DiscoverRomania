using JRS.DR.WsModels;

namespace JRS.DR.Contracts
{
    public interface IApiService
    {
        GetLanguagesResponse GetLanguages();
        GetObjectiveTypesResponse GetObjectiveTypes();
        GetObjectivesResponse GetObjectives(GetObjectivesRequest input);
        GetObjectiveResponse GetObjective(GetObjectiveRequest input);
        GetObjectiveHtmlResponse GetObjectiveHtml(GetObjectiveHtmlRequest input);
    }
}