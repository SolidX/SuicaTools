using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Solidus.SuicaTools.Data.Converters
{
    /// <summary>
    /// Converts <see cref="DateOnly?"/> to <see cref="DateTime?"/> and vice versa
    /// </summary>
    public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
    {
        public NullableDateOnlyConverter() : base(
            d => d == null ? null : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
            dt => dt == null ? null : new DateOnly?(DateOnly.FromDateTime(dt.Value)))
        {
        }
    }
}
