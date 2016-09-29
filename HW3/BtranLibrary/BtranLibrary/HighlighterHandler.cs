using System.IO;
using System.Web;

namespace BtranLibrary
{
	/// <summary>
	/// Highlight items within a particular extension with random colors
	/// </summary>
	public class HighlighterHandler : IHttpHandler
	{
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Process the Handler whenever we get an extension we're listeing to
		/// </summary>
		/// <param name="context"></param>
		public void ProcessRequest(HttpContext context)
		{
			string physicalPath = context.Request.PhysicalPath;

			// extract the contents by reading the file from beginning to end
			StreamReader sr = File.OpenText(physicalPath);
			string content = sr.ReadToEnd();
			sr.Close();

			// format based on extension type
			string extension = Path.GetExtension(physicalPath).ToLower();

			string output = string.Empty;

			switch (extension)
			{
				case ".html":
					// trigger syntax highlighter for HTML files
					output = SyntaxHighlighter.GetHighlighter(SyntaxType.HTML).Highlight(content);
					break;
				case ".cs":
				case ".vb":
				default:
					output = content;
					break;
			}

			context.Response.Write(output);
		}
	}
}
