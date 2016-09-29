using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BtranLibrary
{
	public class HTMLHighlighter : SyntaxHighlighter
	{
		private string[] colors = { "Silver", "Gray", "Black", "Red", "Maroon", "Yellow", "Olive", "Lime", "Green", "Aqua", "Teal", "Blue", "Navy", "Fuchsia", "Purple" };

		private const string PATTERN_PREFIX_HEADER1 = "<h1|<H1";
		private const string PATTERN_PREFIX_HEADER2 = "<h2|<H2";
		private const string PATTERN_PREFIX_HEADER3 = "<h3|<H3";
		private const string PATTERN_PREFIX_HEADER4 = "<h4|<H4";
		private const string PATTERN_PREFIX_HEADER5 = "<h5|<H5";
		private const string PATTERN_PREFIX_HEADER6 = "<h6|<H6";
		private const string PATTERN_PREFIX_PARAGRAPH = "<p|<P";
		private const string PATTERN_PREFIX_LIST_ITEM = "<li|<LI";

		private const string PREFIX_HEADER1 = "<h1";
		private const string PREFIX_HEADER2 = "<h2";
		private const string PREFIX_HEADER3 = "<h3";
		private const string PREFIX_HEADER4 = "<h4";
		private const string PREFIX_HEADER5 = "<h5";
		private const string PREFIX_HEADER6 = "<h6";
		private const string PREFIX_PARAGRAPH = "<p";
		private const string PREFIX_LIST_ITEM = "<li";

		private Random _scratchRandom = new Random();
		private StringBuilder _scratchStringBuilder = new StringBuilder();

		public override string Highlight(string content)
		{
			string replaceStr = content;

			// traverse common HTML Tags and change their colors!
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER1, PREFIX_HEADER1, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER2, PREFIX_HEADER2, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER3, PREFIX_HEADER3, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER4, PREFIX_HEADER4, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER5, PREFIX_HEADER5, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_HEADER6, PREFIX_HEADER6, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_PARAGRAPH, PREFIX_PARAGRAPH, GetRandomColor());
			ConvertPrefixColor(content, ref replaceStr, PATTERN_PREFIX_LIST_ITEM, PREFIX_LIST_ITEM, GetRandomColor());

			// return the completely transformed HTML page
			return replaceStr;
		}

		private string GetRandomColor()
		{
			return colors[_scratchRandom.Next(0, colors.Length)];
		}

		/// <summary>
		/// Set the color for the provided HTML Tag
		/// </summary>
		/// <param name="content">the entire content that we're parsing</param>
		/// <param name="replaceStr">the new entire content</param>
		/// <param name="pattern">the pattern we're looking for</param>
		/// <param name="prefix">the HTML Tag</param>
		/// <param name="color">the color we're changing to</param>
		private void ConvertPrefixColor(string content, ref string replaceStr, string pattern, string prefix, string color)
		{
			if (Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase))
			{
				_scratchStringBuilder.Clear();
				_scratchStringBuilder.Append(prefix);
				_scratchStringBuilder.Append(" style=\"color:");
				_scratchStringBuilder.Append(color);
				_scratchStringBuilder.Append(";\" ");

				replaceStr = Regex.Replace(replaceStr, pattern, _scratchStringBuilder.ToString());

				_scratchStringBuilder.Clear();
			}
		}
	}
}
