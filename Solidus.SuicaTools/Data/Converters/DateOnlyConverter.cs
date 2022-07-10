using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Solidus.SuicaTools.Data.Converters
{
    /// <summary>
    /// Converts <see cref="DateOnly"/> to <see cref="DateTime"/> and vice versa
    /// </summary>
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(d => d.ToDateTime(TimeOnly.MinValue), dt => DateOnly.FromDateTime(dt))
        {
        }
    }
}
