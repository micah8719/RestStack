using System.IO;
using System.Text;

namespace RestStack
{
	internal sealed class CustomEncodingWriter : StringWriter
	{
		public CustomEncodingWriter(StringBuilder stringBuilder, Encoding encoding)
			: base(stringBuilder)
		{
			Encoding = encoding;
		}

		public override Encoding Encoding { get; }
	}
}