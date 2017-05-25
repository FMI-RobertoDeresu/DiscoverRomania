using System.Collections.Generic;
using JRS.DR.WsModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JRS.DR.ViewModels.Admin
{
    public class ObjectiveModel
    {
        public int? ObjectiveId { get; set; }

        public int? LanguageId { get; set; }

        public List<SelectListItem> Languages { get; set; }

        public int? ObjectiveTypeId { get; set; }

        public List<SelectListItem> ObjectiveTypes { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Html { get; set; }

        public Location Location { get; set; }
    }
}