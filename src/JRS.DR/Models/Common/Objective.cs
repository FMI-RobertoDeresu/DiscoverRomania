﻿using System;
using JRS.DR.Models.Enums;

namespace JRS.DR.Models.Common
{
    public class Objective : EntityComplexBase<int>
    {
        protected Objective() { }

        public Objective(Language language, Location location, ObjectiveType type, ObjectiveStatus status, string name,
            string description, string picture, string html)
        {
            Language = language;
            Location = location;
            Type = type;
            Status = status;
            Name = name;
            Description = description;
            Picture = picture;
            Html = html;
        }

        public Language Language { get; protected set; }

        public Location Location { get; protected set; }

        public ObjectiveType Type { get; protected set; }

        private string _status { get; set; }

        public ObjectiveStatus Status
        {
            get { return (ObjectiveStatus) Enum.Parse(typeof(ObjectiveStatus), _status, true); }
            protected set { _status = value.ToString(); }
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Picture { get; protected set; }

        public string Html { get; protected set; }
    }
}