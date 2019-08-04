using System;
using System.IO;
using System.Linq;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Providers.Repositories.Static
{
    public static class StaticRepository
    {
        private static readonly Lazy<Country[]> _countries;

        static StaticRepository()
        {
            Countries = File.ReadAllLines("Countries.txt")
                .Select((s, i) => new Country {Id = i, Name = s}).ToArray();

            Cities = File.ReadAllLines("Cities.txt").Select((city, i) =>
            {
                var index = Random.Next(0, Countries.Length - 1);

                return new City {Id = i, Name = city, Country = Countries.First(c => c.Id == index)};
            }).ToArray();

            var year = DateTime.Now.Year;
            Hotels = File.ReadAllLines("Hotels.txt").Select((hotel, i) =>
            {
                var cityIndex = Random.Next(0, Cities.Length - 1);
                var estYear = Random.Next(1850, year);

                return new Hotel
                {
                    Id = i, Title = hotel, YearOfConstruction = estYear, Address = "Empty",
                    City = Cities.First(city => city.Id == cityIndex)
                };
            }).ToArray();
        }

        public static Random Random => new Random((int) DateTime.Now.Ticks);

        public static Country[] Countries { get; }

        public static City[] Cities { get; }

        public static Hotel[] Hotels { get; }

        public static Tour GetTour(TourProvider provider, long index)
        {
            var currentDate = DateTime.Now;
            var hotelId = Random.Next(0, Hotels.Length);
            var cityOdDepartureId = Random.Next(0, Cities.Length);
            var cityOdArrivalId = Random.Next(0, Cities.Length);
            var appartmentTypeId = Random.Next(0, 3);
            var dateOdDeparture = currentDate.AddDays(Random.Next(0, 200));
            var price = Random.Next(50000, 150000);
            var nights = Random.Next(1, 15);
            var guests = Random.Next(1, 4);

            var tour = new Tour
            {
                Id = index,
                Title = $"{provider.Title} tour {index}",
                Hotel = Hotels.Single(hotel => hotel.Id == hotelId),
                Nights = nights,
                CityOfArrival = Cities.Single(city => city.Id == cityOdArrivalId),
                CityOfDeparture = Cities.Single(city => city.Id == cityOdDepartureId),
                MaxGuestCount = guests,
                Provider = provider,
                ApartmentsType = (ApartmentsType) appartmentTypeId,
                DateOfDeparture = dateOdDeparture,
                PricePerGuest = price
            };

            tour.DateOfArrival = tour.DateOfDeparture.AddHours(Random.Next(3, 13));
            tour.CheckInDate = tour.DateOfArrival.AddHours(Random.Next(1, 8));

            return tour;
        }
    }
}