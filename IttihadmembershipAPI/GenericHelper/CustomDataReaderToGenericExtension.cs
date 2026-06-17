using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.GenericHelper
{
    public class CustomDataReaderToGenericExtension
    {
        public static IList<T> GetDataObjects<T>(SqlDataReader reader) where T : class, new()
        {
            var list = new List<T>();

            if (reader == null || !reader.HasRows)
                return list;

            HashSet<string> tableColumnNames = null;

            while (reader.Read())
            {
                // Pehli row par column names collect karein
                if (tableColumnNames == null)
                {
                    tableColumnNames = CollectColumnNames(reader);
                }

                var entity = new T();
                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    // Check karein ki property name database column se match karta hai
                    if (tableColumnNames.Contains(propertyInfo.Name))
                    {
                        object retrievedObject = reader[propertyInfo.Name];

                        if (retrievedObject != null && retrievedObject != DBNull.Value)
                        {
                            Type targetType = propertyInfo.PropertyType;

                            // Nullable types handle karein (e.g. long?, int?)
                            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                targetType = Nullable.GetUnderlyingType(targetType);
                            }

                            try
                            {
                                // Bool conversion handle karein (Database 0/1 to C# true/false)
                                if (targetType == typeof(bool))
                                {
                                    propertyInfo.SetValue(entity, Convert.ToBoolean(retrievedObject), null);
                                }
                                // Baaki saare normal types ke liye
                                else
                                {
                                    propertyInfo.SetValue(entity, Convert.ChangeType(retrievedObject, targetType), null);
                                }
                            }
                            catch
                            {
                                // Agar ChangeType fail ho jaye toh direct set karne ki koshish karein
                                propertyInfo.SetValue(entity, retrievedObject, null);
                            }
                        }
                        else
                        {
                            // DB NULL ko C# null set karein
                            propertyInfo.SetValue(entity, null, null);
                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        // IS METHOD KO DHYAN SE DEKHEIN (Yahan error aa raha tha)
        private static HashSet<string> CollectColumnNames(SqlDataReader reader)
        {
            var collectedColumnInfo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                collectedColumnInfo.Add(reader.GetName(i));
            }
            return collectedColumnInfo;
        }
    }
}
