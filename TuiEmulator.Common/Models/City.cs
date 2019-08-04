using System.Diagnostics;

namespace TuiEmulator.Common.Models
{
    /// <summary>
    ///     Город
    /// </summary>
    [DebuggerDisplay("Id = {Id}, Name = {Name}, Country = {Country.Name}")]
    public class City
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Страна
        /// </summary>
        public Country Country { get; set; }
    }
}