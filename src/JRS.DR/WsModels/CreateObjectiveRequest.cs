namespace JRS.DR.WsModels
{
    public class CreateObjectiveRequest
    {
        public string Password { get; set; }

        public ObjectiveRequest Objective { get; set; }
    }
}