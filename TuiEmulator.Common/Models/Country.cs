using System.Diagnostics;

namespace TuiEmulator.Common.Models
{
    /// <summary>
    ///     Страна
    /// </summary>
    [DebuggerDisplay("Id = {Id}, Name = {Name}")]
    public class Country
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string Name { get; set; }
    }
}