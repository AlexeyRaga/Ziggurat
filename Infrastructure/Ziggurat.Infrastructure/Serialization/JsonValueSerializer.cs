using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Serialization
{
    public sealed class JsonValueSerializer : ISerializer
    {
        static class Serializers
        {
            public static readonly JsonSerializer Serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Auto,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public void Serialize(object value, Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.CloseOutput = false;

                try
                {
                    Serializers.Serializer.Serialize(jsonWriter, value);
                    streamWriter.Flush();
                }
                catch (JsonWriterException ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }

        public object Deserialize(Type valueType, Stream stream)
        {
            var streamReader = new StreamReader(stream);
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                jsonReader.CloseInput = false;

                try
                {
                    return Serializers.Serializer.Deserialize(jsonReader, valueType);
                }
                catch (JsonException ex)
                {
                    throw new SerializationException(ex.Message, ex);
                }
            }
        }
    }
}
