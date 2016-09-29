using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BtranLibrary
{
	public class CSharpHighlighter : SyntaxHighlighter
	{
		private string[] keywords = { " for", " do ", "while", " if ", " else ", " else if " };
		private string[] dataTypes = { "int ", "double " };
		private string[] lineTerminators = { ";", "{", "}" };

		public override string Highlight(string contents)
		{
			string replaceStr = contents;
			int indentLevelCount = 0;

			//
			// highlight keywords in blue
			//
			foreach (string s in keywords)
			{
				if (Regex.IsMatch(contents, s, RegexOptions.IgnoreCase))
				{
					replaceStr = Regex.Replace(replaceStr, s, "<span style='color:blue'>" + s + "</span>");
				}
			}

			//
			// highlight data types in red
			//
			foreach (string s in dataTypes)
			{
				if (Regex.IsMatch(contents, s, RegexOptions.IgnoreCase))
				{
					replaceStr = Regex.Replace(replaceStr, s, "<span style='color:red'>" + s + "</span>");
				}
			}

			//
			// highlight comments in green and put them on new lines
			//
			StringReader reader = new StringReader(replaceStr);
			string line = string.Empty;
			StringBuilder sb = new StringBuilder(string.Empty);
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Contains("//") || line.Contains("/*"))
				{
					sb.Append("<span style='color:green'>" + line + "</span><br>");
				}
				else
				{
					sb.AppendLine(line);
				}
			}
			replaceStr = sb.ToString();

			reader = null;
			sb = null;

			//
			// add indentation to enhance readability
			//
			// NOTE: better indentation would probably require
			// a style sheet. Are you up to giving this a shot
			// dear students?? :-)
			//
			reader = new StringReader(replaceStr);
			line = string.Empty;
			sb = new StringBuilder(string.Empty);
			while ((line = reader.ReadLine()) != null)
			{
				if (line.Contains("}"))
				{
					indentLevelCount--;
					sb.AppendLine(line + "</BLOCKQUOTE>");
				}
				else
				{
					if (line.Contains("{"))
					{
						indentLevelCount++;
						sb.Append("<BLOCKQUOTE>" + line);
					}
					else
					{
						sb.AppendLine(line);
					}
				}
			}
			replaceStr = sb.ToString();

			reader = null;
			sb = null;

			//
			// put curly braces on new lines
			//
			foreach (string s in lineTerminators)
			{
				if (Regex.IsMatch(contents, s, RegexOptions.IgnoreCase))
				{
					if (s.Contains("{"))
					{
						replaceStr = Regex.Replace(replaceStr, s, "<br>" + s + "\n<br>");
					}
					else
					{
						replaceStr = Regex.Replace(replaceStr, s, s + "\n<br>");
					}
				}
			}
			return replaceStr;
		}
	}
}
