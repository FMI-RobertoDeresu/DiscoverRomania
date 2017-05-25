using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JRS.DR.WsModels;

namespace JRS.DR.ViewModels.Admin
{
    public class ObjectiveLightModel
    {
        public int? ObjectiveId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
