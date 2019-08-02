using System;

namespace TuiEmulator.Common.Models.Requests
{
    public class SearchRequest
    {
        public OrderBy OrderBy { get; set; } = OrderBy.Date;
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Asc;
        public SearchFields Fields { get; set; }

        public int Take { get; set; } = 1000;

        public class SearchFields
        {
            public int? CityOfDepartureId { get; set; }
            public int? CityOfTourId { get; set; }
            public DateTimeOffset? TourStartDate { get; set; }
            public int? NightsFrom { get; set; }
            public int? NightsTo { get; set; }
            public int? GuestsCount { get; set; }
        }
    }
}