using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtranLibrary
{
	public enum SyntaxType
	{
		VisualBasic,
		CSharp,
		HTML
	}

	public class SyntaxHighlighter
	{
		public virtual string Highlight(string contents)
		{
			return string.Empty;
		}

		public static SyntaxHighlighter GetHighlighter(SyntaxType synType)
		{
			SyntaxHighlighter sh = null;

			switch (synType)
			{
				case SyntaxType.CSharp:
					sh = new CSharpHighlighter();
					break;
				case SyntaxType.HTML:
					sh = new HTMLHighlighter();
					break;
				case SyntaxType.VisualBasic:
					break;
				default:
					break;
			}

			return sh;
		}
	}
}
