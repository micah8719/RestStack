using System.Text;

namespace RestStack
{
	internal interface ISerializer
	{
		Encoding Encoding { get; }

		object Deserialize(string value);
		string Serialize(object value);
	}
}