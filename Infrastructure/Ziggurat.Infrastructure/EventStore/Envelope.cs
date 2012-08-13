using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ziggurat.Infrastructure.EventStore
{
    public sealed class Envelope
    {
        public bool IsAggregateIdSet { get { return Headers.ContainsKey(EventHeaderKeys.AggregateId); } }
        public Guid AggregateId
        {
            get { return GetValueFromHeaders<Guid>(EventHeaderKeys.AggregateId); }
            set { Headers[EventHeaderKeys.AggregateId] = value; }
        }

        public bool IsDateCreatedSet { get { return Headers.ContainsKey(EventHeaderKeys.DateCreated); } }
        public DateTime DateCreated
        {
            get { return GetValueFromHeaders<DateTime>(EventHeaderKeys.DateCreated); }
            set { Headers[EventHeaderKeys.DateCreated] = value; }
        }

        public object Body { get; set; }
        public IDictionary<string, object> Headers { get; set; }

        public Envelope()
        {
            Headers = new Dictionary<string, object>();
        }

        public Envelope(object body, IDictionary<string, object> extraHeaders)
        {
            Body = body;
            Headers = extraHeaders == null 
                ? new Dictionary<string, object>() 
                : extraHeaders.ToDictionary(x => x.Key, x => x.Value);
        }

        private T GetValueFromHeaders<T>(string key)
        {
            object rawValue;
            if (!Headers.TryGetValue(key, out rawValue) || !(rawValue is T)) 
                return default(T);

            return (T)rawValue;
        }
    }
}
