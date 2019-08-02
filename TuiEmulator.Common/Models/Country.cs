using System.Diagnostics;

namespace TuiEmulator.Common.Models
{
    [DebuggerDisplay("Id = {Id}, Name = {Name}")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}