using System;
using System.Collections.Generic;
using System.Linq;

namespace Ziggurat.Infrastructure.EventStore
{
    public static class EventHeaderKeys
    {
        public static string Stamp = "StampES";
        public static string UniqueId = "UniqueId";

        public static string AggregateId = "AggregateId";
        public static string DateCreated = "DateCreated";
        public static string MemberId = "MemberId";
    }

    /// <summary>
    /// A message (event) envelope that contains a message itself + some "metadata".
    /// Every message coming in or out of EventStore is wrapped into this envelope.
    /// </summary>
    public sealed class Envelope
    {
        //Get or set some metadata.
        //Getter and setter are defined as functions, not as properties to avoid serialization problems
        //Question: move them to some extention methods? Or it doesn't make sense as there is only 1 envelope type?

        public bool IsAggregateIdSet() { return Headers.ContainsKey(EventHeaderKeys.AggregateId); }
        public Guid GetAggregateId() { return GetValueFromHeaders<Guid>(EventHeaderKeys.AggregateId); }
        public void SetAggregateId(Guid id) { Headers[EventHeaderKeys.AggregateId] = id; }

        public bool IsDateCreatedSet() { return Headers.ContainsKey(EventHeaderKeys.DateCreated); }
        public DateTime GetDateCreated() { return GetValueFromHeaders<DateTime>(EventHeaderKeys.DateCreated); }
        public void SetDateCreated(DateTime dateCreated) { Headers[EventHeaderKeys.DateCreated] = dateCreated; }

        public void SetStamp(long stamp) { Headers[EventHeaderKeys.Stamp] = stamp; }
        public long GetStamp() { return GetValueFromHeaders<long>(EventHeaderKeys.Stamp); }

        public void SetUniqueId(string uniqueId) { Headers[EventHeaderKeys.UniqueId] = uniqueId; }
        public string GetUniqueId() { return GetValueFromHeaders<string>(EventHeaderKeys.UniqueId); }

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
