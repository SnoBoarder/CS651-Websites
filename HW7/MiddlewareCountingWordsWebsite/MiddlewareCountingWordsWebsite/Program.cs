using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareCountingWordsWebsite
{
    using Microsoft.Owin;
    using Microsoft.Owin.Hosting;
    using Owin;
    using System.Collections;
    using System.IO;
    using System.Text.RegularExpressions;
    // use an alias for the OWIN AppFunc:
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Program
    {
        public const string ADDRESS = "http://localhost:8080";

        static void Main(string[] args)
        {
            WebApp.Start<Startup>(ADDRESS);
            Console.WriteLine("Server Started; Press enter to Quit");
            Console.ReadLine();
        }
    }

    public class Startup
    {
        private const string AUTHENTICATION_CODE = "dino";
        private const string PARAGRAPH_SEPARATOR = "/";
        private const string REMOVE_AUTHENTICATION = AUTHENTICATION_CODE + PARAGRAPH_SEPARATOR;

        public void Configuration(IAppBuilder app)
        {
            var loggingMiddleWare = new Func<AppFunc, AppFunc>(LoggingMiddleWare);
            var authenticationMiddleWare = new Func<AppFunc, AppFunc>(AuthenticationMiddleWare);
            var myMiddleWare = new Func<AppFunc, AppFunc>(MyMiddleWare);

            app.Use(loggingMiddleWare);
            app.Use(authenticationMiddleWare);
            app.Use(myMiddleWare);
        }

        public AppFunc LoggingMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                // Call the next Middleware in the chain:
                await next.Invoke(environment);

                // Do the logging on the way out:
                IOwinContext context = new OwinContext(environment);
                Console.WriteLine("URI: {0} Status Code: {1}",
                context.Request.Uri, context.Response.StatusCode);
            };

            return appFunc;
        }

        public AppFunc AuthenticationMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                IOwinContext context = new OwinContext(environment);

                string[] query = context.Request.QueryString.Value.Split(Convert.ToChar(PARAGRAPH_SEPARATOR));

                var isAuthorized = query[0] == AUTHENTICATION_CODE;
                if (!isAuthorized)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ReasonPhrase = "Not Authorized";

                    // Send back an error page:
                    await context.Response.WriteAsync(string.Format("<h1>Error {0}-{1}",
                        context.Response.StatusCode,
                        context.Response.ReasonPhrase));
                }
                else
                {
                    // we will only continue if authentication succeeds:
                    context.Response.StatusCode = 200;
                    context.Response.ReasonPhrase = "OK";
                    await next.Invoke(environment);
                }
            };

            return appFunc;
        }

        public AppFunc MyMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                IOwinContext context = new OwinContext(environment);

                string sourceString = context.Request.QueryString.Value;
                string cleanContent = string.Empty;
                int index = sourceString.IndexOf(REMOVE_AUTHENTICATION);
                if (index < 0)
                {
                    index = sourceString.IndexOf(AUTHENTICATION_CODE);
                    if (index < 0)
                    {
                        cleanContent = sourceString;
                    }
                    else
                    {
                        cleanContent = sourceString.Remove(index, AUTHENTICATION_CODE.Length);
                    }
                }
                else
                {
                    cleanContent = sourceString.Remove(index, REMOVE_AUTHENTICATION.Length);
                }

                cleanContent = Uri.UnescapeDataString(cleanContent);

                string wordCount = CountingWords.FromInput(cleanContent);

                // Do something with the incoming result:
                var response = environment["owin.ResponseBody"] as Stream;

                using (var writer = new StreamWriter(response))
                {
                    // loading the entire HTML piecemeal to integrate key parts (url, input, and output)
                    string html1 = @"
                        <!DOCTYPE html>
                        <!-- Developed by Brian Tran [btran89@bu.edu] -->
                        <html>
	                        <head>
		                        <title></title>
		                        <style>
			                        #container {
				                        width: 100%;
				                        font-family: Arial, Helvetica, sans-serif;
				                        color: #FFF;
				                        margin: auto;
			                        }

			                        #header {
				                        background-color: #D9853B;
				                        width: 100%;
				                        height: 100px;
				                        color: #FFF;
				                        display: inline-block;
				                        padding-left: 1%;
				                        box-sizing: border-box;
			                        }

			                        #content {
				                        background-color: #558C89;
				                        padding-left: 1%;
				                        padding-right: 1%;
				                        padding-bottom: 1%;
				                        margin-bottom: 1%;
				                        width: 50%;
				                        height: 600px;
				                        float: left;
				                        box-sizing: border-box;
			                        }

			                        #contentb {
				                        background-color: #74AFAD;
				                        padding-left: 1%;
				                        padding-right: 1%;
				                        padding-bottom: 1%;
				                        margin-bottom: 1%;
				                        width: 50%;
				                        height: 600px;
				                        float: left;
				                        box-sizing: border-box;
			                        }

			                        #footer {
				                        clear: left;
				                        background-color: #D9853B;
				                        width: 100%;
				                        padding-left: 1%;
				                        padding-top: 1%;
				                        padding-bottom: 1%;
				                        display: block;
				                        box-sizing: border-box;
			                        }

			                        .input {
				                        width: 98%;
				                        height: 500px;
				                        color: white;
				                        background-color: inherit;
				                        resize: none;
				                        padding-left: 1%;
				                        padding-right: 1%;
				                        padding-bottom: 1%;
			                        }

			                        .output {
				                        width: 98%;
				                        height: 500px;
				                        color: white;
				                        background-color: inherit;
				                        resize: none;
				                        padding-left: 1%;
				                        padding-right: 1%;
				                        padding-bottom: 1%;
			                        }

			                        @media screen and (max-width: 490px) {
				                        #container {
					                        width: 100%;
				                        }

				                        h1 {
					                        font-size: 1.2em;
				                        }

				                        h2 {
					                        font-size: 1em;
				                        }
			                        }

			                        @media screen and (max-width: 800px) {
				                        #container {
					                        max-width: 490px;
				                        }

				                        #content {
					                        width: 100%;
				                        }

				                        #contentb {
					                        width: 100%;
					                        margin: 0;
				                        }
			                        }
		                        </style>
		                        <script type='text/javascript'>
			                        function moveCaretToEnd(el) {
			                            if (typeof el.selectionStart == 'number') {
			                                el.selectionStart = el.selectionEnd = el.value.length;
			                            } else if (typeof el.createTextRange != 'undefined') {
			                                el.focus();
			                                var range = el.createTextRange();
			                                range.collapse(false);
			                                range.select();
			                            }
			                        }

			                        function refreshWordCount(el) {
                                        if (el.value[el.value.length - 1] != ' ')
				                        window.location.assign(";

                    string address = "\"" + Program.ADDRESS + "/?" + REMOVE_AUTHENTICATION + "\" + el.value";

                    string html2 = @" );
			                        }
		                        </script>
		                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />

	                        </head>
	                        <body>
		                        <form id='form1' runat='server'>
			                        <div id='container'>
				                        <div id='header'>
					                        <h1>Brian Tran's Word Counting</h1>
				                        </div>

				                        <div id='content'>
					                        <h1>Input</h1>
					                        <textarea id='input' rows='20' cols='48' autofocus='true' onfocus='moveCaretToEnd(this);' onkeyup='refreshWordCount(this)'>";

                    string html3 = @"</textarea>
				                        </div>

				                        <div id='contentb'>
					                        <h1>Output</h1>
					                        <textarea id='output' rows='20' cols='50' readonly='true'>";

                    string html4 =@"</textarea>
				                        </div>

				                        <div id='footer'>
					                        <p>Copyright Brian Tran</p>
				                        </div>
			                        </div>
		                        </form>
	                        </body>
                        </html>";

                    // concatenate all the parts of the html
                    string concatenatedHTML = html1 + address + html2 + cleanContent + html3 + wordCount + html4;

                    await writer.WriteAsync(concatenatedHTML);
                }

                // Call the next Middleware in the chain:
                await next.Invoke(environment);
            };

            return appFunc;
        }
    }

    public class CountingWords
    {
        private static StringBuilder _scratchStringBuilder = new StringBuilder();
        private static Hashtable _scratchHashtable = new Hashtable();

        public static string FromInput(string input)
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
}
