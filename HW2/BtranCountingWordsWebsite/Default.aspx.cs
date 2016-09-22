using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
	private static StringBuilder _scratchStringBuilder;

	private static Hashtable _scratchHashtable;

	private string output;

	protected void Page_Load(object sender, EventArgs e)
	{
		_scratchStringBuilder = new StringBuilder();
		_scratchHashtable = new Hashtable();

		// TODO: Ask about this
		//ClientScript.GetPostBackEventReference(this, string.Empty);
	}

	protected void Submit(object sender, EventArgs e)
	{
		CountWords(_scratchHashtable, InputField.Text);
		OutputField.Text = GenerateOutput(_scratchHashtable);
	}

	private static void CountWords(Hashtable hashTable, string words)
	{
		// clear scratch hashtable
		hashTable.Clear();

		// strip out all special characters from the string
		string cleanWords = Regex.Replace(words, "[^0-9a-zA-Z ]+", "");

		string[] wordArray = cleanWords.Split(' ');

		string word;
		for (int i = 0; i < wordArray.Length; ++i)
		{
			word = wordArray[i];

			if (word.Length > 1)
				word = word.ToLower();

			if (!hashTable.ContainsKey(word))
			{
				hashTable[word] = 1;
			}
			else
			{
				hashTable[word] = (int)hashTable[word] + 1;
			}
		}
	}

	private static string GenerateOutput(Hashtable hashTable)
	{
		_scratchStringBuilder.Clear();

		foreach (DictionaryEntry e in hashTable)
		{
			_scratchStringBuilder.Append(e.Key);
			_scratchStringBuilder.Append(" : ");
			_scratchStringBuilder.Append(e.Value);
			_scratchStringBuilder.Append("\n");
		}

		return _scratchStringBuilder.ToString();
	}
}