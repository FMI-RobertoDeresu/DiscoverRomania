namespace JRS.DR.WsModels
{
    public class ObjectiveResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public Location Location { get; set; }

        public ObjectiveTypeResponse Type { get; set; }
    }
}