using System.IO;
using Newtonsoft.Json;

namespace Ziggurat.Infrastructure.Projections
{
    public sealed class JsonProjectionSerializer : IProjectionSerializer
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

		public void Serialize<TView>(TView view, Stream stream)
		{
		    var streamWriter = new StreamWriter(stream);
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.CloseOutput = false;

				try
				{
					Serializers.Serializer.Serialize(jsonWriter, view);
					streamWriter.Flush();
				} 
				catch(JsonWriterException ex)
				{
					throw new ProjectionSerializationException(ex.Message, ex);
				}
            }
		}

		public TView Deserialize<TView>(Stream stream)
		{
		    var streamReader = new StreamReader(stream);
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                jsonReader.CloseInput = false;

				try
				{
					return Serializers.Serializer.Deserialize<TView>(jsonReader);
				} 
				catch (JsonException ex)
				{
					throw new ProjectionSerializationException(ex.Message, ex);
				}
            }
		}
	}
}
