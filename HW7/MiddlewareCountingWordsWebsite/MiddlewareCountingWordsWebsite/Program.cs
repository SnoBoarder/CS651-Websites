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
        static void Main(string[] args)
        {
            WebApp.Start<Startup>("http://localhost:8080");
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
                int index = context.Request.QueryString.Value.IndexOf(REMOVE_AUTHENTICATION);
                string cleanContent = (index < 0) ? sourceString : sourceString.Remove(index, REMOVE_AUTHENTICATION.Length);

                cleanContent = Uri.UnescapeDataString(cleanContent);

                string wordCount = CountingWords.FromInput(cleanContent);

                // Do something with the incoming result:
                var response = environment["owin.ResponseBody"] as Stream;
                using (var writer = new StreamWriter(response))
                {
                    // TODO: load css and html into writesync lol
                    await writer.WriteAsync("<h1>Hello Dino!</h1>");
                    await writer.WriteAsync("<p>" + cleanContent + "</p>");
                    await writer.WriteAsync("<p>" + wordCount + "</p>");
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
