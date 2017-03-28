namespace JRS.DR.WsModels
{
    public class ObjectiveResponse
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public LocationResponse Location { get; set; }

        public ObjectiveTypeResponse Type { get; set; }
    }
}