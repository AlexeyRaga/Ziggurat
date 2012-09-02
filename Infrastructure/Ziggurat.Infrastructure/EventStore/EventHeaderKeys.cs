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
}
