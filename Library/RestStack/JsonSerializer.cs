using System.Text;

using Newtonsoft.Json;

namespace RestStack
{
	public class JsonSerializer<T> : SerializerBase<T>
	{
		private readonly JsonSerializerSettings _settings;

		public JsonSerializer(JsonSerializerSettings settings)
			: base(Encoding.UTF8)
		{
			_settings = settings;
		}

		public JsonSerializer()
			: this(null)
		{
		}

		public override T Deserialize(string value)
		{
			return _settings != null
				? JsonConvert.DeserializeObject<T>(value, _settings)
				: JsonConvert.DeserializeObject<T>(value);
		}

		public override string Serialize(T value)
		{
			return _settings != null
				? JsonConvert.SerializeObject(value, _settings)
				: JsonConvert.SerializeObject(value);
		}
	}
}