using System.Diagnostics;

namespace TuiEmulator.Common.Models
{
    [DebuggerDisplay("Id = {Id}, Name = {Name}, Country = {Country.Name}")]
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}