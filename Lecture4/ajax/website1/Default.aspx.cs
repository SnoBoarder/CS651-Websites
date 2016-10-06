using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
	public string postBackString = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		// register the javascript events when page loads
		TextBox1.Attributes.Add("onkeyup", "OnKeyUp()");
		TextBox1.Attributes.Add("onfocus", "SetEnd(this)");

		// get the postback function reference and pass this back to the client
		postBackString = Page.ClientScript.GetPostBackEventReference(TextBox1, TextBox1.Text);

		if (IsPostBack)
		{
			GetWords(TextBox1.Text);
		}
	}

	private void GetWords(string content)
	{
		Result.Text = content.ToUpper();
	}

	protected void OnButtonClick(object sender, EventArgs e)
	{
		GetWords(TextBox1.Text);
	}
}