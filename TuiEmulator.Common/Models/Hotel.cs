namespace TuiEmulator.Common.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public int YearOfConstruction { get; set; }
    }
}