using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
	ServiceReference1.Service1Client s = new ServiceReference1.Service1Client();
	//ServiceReference1.Service1Client s = new ServiceReference1.Service1Client();

	public static int[] array;

	public static bool freeModeEnabled { get; private set; }

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			freeModeEnabled = false;

			Response.Write("Welcome to Brian's app!");
			//_sentence.Text = "This is a sentence.";
			_letter.Text = "e";
		}
		else
		{
			Response.Write("You removed letter(s) from the sentence!");
		}
	}

	protected void OnRemoveLetter(object sender, EventArgs e)
	{
		_sentence.Text =  s.RemoveLetterFromSentence(_letter.Text, _sentence.Text);
	}

	protected void OnHello(object sender, EventArgs e)
	{
		array = s.GetHello();
	}

	protected void OnGoodBye(object sender, EventArgs e)
	{
		array = s.GetGood();
	}

	protected void OnFreeMode(object sender, EventArgs e)
	{
		freeModeEnabled = !freeModeEnabled;
	}
}

public static class JavaScript
{
	public static string Serialize(object o)
	{
		JavaScriptSerializer js = new JavaScriptSerializer();
		return js.Serialize(o);
	}
}