using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRS.DR.ViewModels.Admin
{
    public class ObjectivesListModel
    {
        public List<ObjectiveLightModel> Objectives { get; set; }

        public int Page { get; set; }
    }
}
