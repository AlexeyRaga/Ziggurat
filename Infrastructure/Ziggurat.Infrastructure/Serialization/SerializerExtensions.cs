using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Serialization
{
    public static class SerializerExtensions
    {
        public static void Serialize<TValue>(this ISerializer serializer, TValue value, Stream stream)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            serializer.Serialize(value, stream);
        }

        public static TValue Deserialize<TValue>(this ISerializer serializer, Stream stream)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            return (TValue)serializer.Deserialize(typeof(TValue), stream);
        }

        public static byte[] SerializeToByteArray(this ISerializer serializer, object value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(value, stream);
                return stream.ToArray();
            }
        }

        public static object Deserialize(this ISerializer serializer, Type valueType, byte[] value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            using (var stream = new MemoryStream(value))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(valueType, stream);
            }
        }

        public static TValue Deserialize<TValue>(this ISerializer serializer, byte[] value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            return (TValue)serializer.Deserialize(typeof(TValue), value);
        }

        public static string SerializeToString(this ISerializer serializer, object value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(value, stream);
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }

        public static object Deserialize(this ISerializer serializer, Type valueType, string value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(value);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    return serializer.Deserialize(valueType, stream);
                }
            }
        }

        public static TValue Deserialize<TValue>(this ISerializer serializer, string value)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            return (TValue)serializer.Deserialize(typeof(TValue), value);
        }
    }
}
