using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
	protected string _wordCountResults;

	protected void Page_Load(object sender, EventArgs e)
	{
		ClientScriptManager csm = Page.ClientScript;
		string cbReference = csm.GetCallbackEventReference(this, "arg", "ReceiveServerData", "");
		string callbackScript = "function CallServer(arg) {" + cbReference + "; }";
		csm.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript, true);
	}

	public string GetCallbackResult()
	{
		return _wordCountResults;
	}

	public void RaiseCallbackEvent(string eventArgument)
	{
		_wordCountResults = doWork(eventArgument);
	}

	private string doWork(string str)
	{
		return str.ToUpper();
	}
}