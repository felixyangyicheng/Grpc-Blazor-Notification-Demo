using RealTime_D3.Models;
using System.Data;

namespace RealTime_D3.Data
{
    public static class Utilities
    {
        public static List<Tbllog> DataReaderMapToList<Tentity>(IDataReader reader)
        {
            var results = new List<Tbllog>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<Tbllog>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(Tbllog).GetProperties())
                    {
                        if ((typeof(Tbllog).GetProperty(property.Name??throw new NullReferenceException("property.Name is null")).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return results;
        }
    }
}
