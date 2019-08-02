using System;

namespace TuiEmulator.Common.Models
{
    public class Tour
    {
        public string Title { get; set; }
        public TourProvider Provider { get; set; }
        public Hotel Hotel { get; set; }
        public ApartmentsType ApartmentsType { get; set; }
        public City CityOfDeparture { get; set; }
        public City CityOfArrival { get; set; }
        public DateTimeOffset DateOfDeparture { get; set; }
        public DateTimeOffset DateOfArrival { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public int Nights { get; set; }
        public decimal PricePerGuest { get; set; }
        public int MaxGuestCount { get; set; }
    }
}