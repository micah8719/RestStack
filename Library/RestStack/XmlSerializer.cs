using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RestStack
{
	public class XmlSerializer<T> : SerializerBase<T>
	{
		private readonly XmlSerializer _serializer;

		public XmlSerializer(Encoding encoding) 
			: base(encoding)
		{
			_serializer = new XmlSerializer(typeof(T));
		}

		public override T Deserialize(string value)
		{
			using (var stringReader = new StringReader(value))
			{
				return (T) _serializer.Deserialize(stringReader);
			}
		}

		public override string Serialize(T value)
		{
			var stringBuilder = new StringBuilder();

			using (var stringWriter = new CustomEncodingWriter(stringBuilder, Encoding))
			{
				_serializer.Serialize(stringWriter, value);

				return stringBuilder.ToString();
			}
		}
	}
}