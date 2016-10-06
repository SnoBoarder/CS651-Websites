using System;
using System.IO;
using System.Security.Principal;
using System.Web;

namespace CustomAuthentication
{
	public class AuthenticationModule : IHttpModule
	{
		private const string USER_ID_KEY = "userid";
		private const string PASSWORD_KEY = "password";

		private const string ROLE_ADMIN = "Administrator";
		private const string ROLE_USER = "User";

		private const string BEGIN_REQUEST_RESPONSE = "Initiating Custom Authentication Module.<br>";
		private const string AUTHENTICATION_FAILED_NOT_PROVIDED_RESPONSE = "<h1>No credentials were provided. Flagging your IP address.</h1>";
		private const string AUTHENTICATION_FAILED_NO_DATA_RESPONSE = "<h1>We are sorry but we could not find this user id and password in our database</h1>";
		private const string AUTHENTICATION_SUCCESS_RESPONSE = "<h1>You have provided valid credentials.</h1><br/>The information you requested is: <h2>Code Blue</h2>.";

		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpApplication app = sender as HttpApplication;
			app.Context.Response.Write(BEGIN_REQUEST_RESPONSE);
		}

		private void OnAuthenticateRequest(object sender, EventArgs e)
		{
			// Authenticate user credentials, and find out user roles
			HttpApplication app = (HttpApplication)sender;
			HttpContext context = (HttpContext)app.Context;

			if ((app.Request[USER_ID_KEY] == null) || (app.Request[PASSWORD_KEY] == null))
			{
				context.Response.Write(AUTHENTICATION_FAILED_NOT_PROVIDED_RESPONSE);
				context.Response.End();
			}

			string userid = app.Request[USER_ID_KEY].ToString();
			string password = app.Request[PASSWORD_KEY].ToString();

			string[] strRoles = AuthenticateAndGetRoles(userid, password);

			if ((strRoles == null) || (strRoles[0].Length == 0))
			{
				context.Response.Write(AUTHENTICATION_FAILED_NO_DATA_RESPONSE);
				app.CompleteRequest();
			}
			else
			{
				context.Response.Write(AUTHENTICATION_SUCCESS_RESPONSE);

				// extract the contents by reading the file from beginning to end
				StreamReader sr = File.OpenText(context.Request.PhysicalPath);
				string content = sr.ReadToEnd();
				sr.Close();

				context.Response.Write(content);

				GenericIdentity objIdentity = new GenericIdentity(userid, "CustomAuthentication");
				context.User = new GenericPrincipal(objIdentity, strRoles);

				app.CompleteRequest();
			}
		}

		private string[] AuthenticateAndGetRoles(string userId, string password)
		{
			string[] strRoles = null;

			if (userId.Equals("dino") && password.Equals("rocks"))
			{
				strRoles = new string[1] { ROLE_ADMIN };
			}
			else if (userId.Equals("dino") && password.Equals("chills"))
			{
				strRoles = new string[1] { ROLE_USER };
			}

			return strRoles;
		}

		#region IHttpModule Interface

		public void Init(HttpApplication context)
		{
			context.BeginRequest += OnBeginRequest;
			context.AuthenticateRequest += OnAuthenticateRequest;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
