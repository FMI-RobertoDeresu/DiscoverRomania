namespace JRS.DR.WsModels
{
    public class CreateObjectiveRequest
    {
        public int? LanguageId { get; set; }

        public int? ObjectiveTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Html { get; set; }

        public Location Location { get; set; }
    }
}