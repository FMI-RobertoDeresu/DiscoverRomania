using JRS.DR.WsModels;

namespace JRS.DR.Contracts
{
    public interface IApiService
    {
        GetLanguagesResponse GetLanguages();
        GetObjectiveTypesResponse GetObjectiveTypes(GetObjectiveTypesRequest input);

        GetObjectiveResponse GetObjective(GetObjectiveRequest input);
        GetObjectivesResponse GetObjectives(GetObjectivesRequest input);
        GetObjectiveHtmlResponse GetObjectiveHtml(GetObjectiveHtmlRequest input);

        CreateObjectiveResponse CreateObjective(CreateObjectiveRequest input);
        EditObjectiveResponse EditObjective(EditObjectiveRequest input);
        DeleteObjectiveResponse DeleteObjective(DeleteObjectiveRequest input);
    }
}