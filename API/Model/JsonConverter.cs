using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace API.Model
{
    public class JsonConverter<T> : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;
            if (primitive == null) return default (T);

            if (primitive.Type != DynamoDBEntryType.String)
            {
                throw new InvalidCastException($"{primitive.Type} with a value of {primitive.Value} can not be converted");
            }

            string json = primitive.AsString();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            T item = (T)value;
            if (item == null) return null;

            string json = JsonConvert.SerializeObject(item);
            return new Primitive(json);
        }

    }

}
