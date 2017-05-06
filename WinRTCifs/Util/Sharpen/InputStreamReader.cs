using System.IO;
using System.Text;
using Windows.Storage;

namespace WinrtCifs.Util.Sharpen
{
    public class InputStreamReader : StreamReader
	{
        protected InputStreamReader(string file):base(Stream.Null)
		{
           throw  new FileNotFoundException();
		}

		public InputStreamReader (InputStream s) : base(s.GetWrappedStream ())
		{
		}

		public InputStreamReader (InputStream s, string encoding) : base(s.GetWrappedStream (), Encoding.GetEncoding (encoding))
		{
		}

		public InputStreamReader (InputStream s, Encoding e) : base(s.GetWrappedStream (), e)
		{
		}
	}
}
