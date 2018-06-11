using System.Text;

namespace RestStack
{
	public abstract class SerializerBase<T> : ISerializer
	{
		protected SerializerBase(Encoding encoding)
		{
			Encoding = encoding;
		}

		public Encoding Encoding { get; }

		public abstract T Deserialize(string value);
		public abstract string Serialize(T value);

		object ISerializer.Deserialize(string value)
		{
			return Deserialize(value);
		}

		string ISerializer.Serialize(object value)
		{
			return Serialize((T) value);
		}
	}
}