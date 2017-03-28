using System;

namespace JRS.DR.Models.Common
{
    public class Language : EntityComplexBase<int>
    {
        protected Language() { }

        public Language(Enums.Language key, string name)
        {
            Key = key;
            Name = name;
        }

        private string _key { get; set; }

        public Enums.Language Key
        {
            get { return (Enums.Language) Enum.Parse(typeof(Enums.Language), _key, true); }
            protected set { _key = value.ToString(); }
        }

        public string Name { get; protected set; }

        public string FlagClass { get; protected set; }
    }
}