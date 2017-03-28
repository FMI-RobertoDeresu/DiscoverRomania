namespace JRS.DR.Models.Common
{
    public class ObjectiveType : EntityComplexBase<int>
    {
        protected ObjectiveType() { }

        public ObjectiveType(string name, string iconClass, Language language)
        {
            Name = name;
            IconClass = iconClass;
            Language = language;
        }

        public string Name { get; protected set; }

        public string IconClass { get; protected set; }

        public Language Language { get; protected set; }
    }
}