using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue
{
    public sealed class JsonQueueMessageSerializer : IQueueMessageSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonQueueMessageSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.Auto,
            };
        }

        public byte[] Serialize(object message)
        {
            var envelope = new JsonQueueMessageEnvelope(message);
            var json = JsonConvert.SerializeObject(envelope, _settings);
            return GetBytes(json);
        }

        public object Deserialize(byte[] message)
        {
            var json = GetString(message);
            var envelope = JsonConvert.DeserializeObject<JsonQueueMessageEnvelope>(json);

            return envelope.Body;
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private sealed class JsonQueueMessageEnvelope
        {
            public object Body { get; set; }
            public JsonQueueMessageEnvelope(object body) { Body = body; }

            public JsonQueueMessageEnvelope() { }
        }
    }
}
