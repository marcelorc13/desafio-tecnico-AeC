using System.Text;

namespace App.Helpers;

public static class CsvHelper
{
    public static byte[] Generate<T>(IEnumerable<T> records)
    {
        var properties = typeof(T).GetProperties();
        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

        foreach (var record in records)
        {
            var values = properties.Select(p =>
            {
                var value = p.GetValue(record)?.ToString() ?? string.Empty;
                return value.Contains(',') ? $"\"{value}\"" : value;
            });
            sb.AppendLine(string.Join(",", values));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
