using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

	public WebService()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	[WebMethod]
	public string HelloWorld()
	{
		return "Hello World";
	}

	private static StringBuilder _scratchStringBuilder = new StringBuilder();
	private static Hashtable _scratchHashtable = new Hashtable();

	[WebMethod]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string CountWordsFromInput(string input)
	{
		CountWords(_scratchHashtable, input);
		return GenerateOutput(_scratchHashtable);
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
