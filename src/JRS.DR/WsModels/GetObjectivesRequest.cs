using System.Collections.Generic;

namespace JRS.DR.WsModels
{
    public class GetObjectivesRequest
    {
        public List<int> FilteredTypes { get; set; }

        public decimal? XLeftTop { get; set; }

        public decimal? YLeftTop { get; set; }

        public decimal? XRightBottom { get; set; }

        public decimal? YRightBottom { get; set; }
    }
}