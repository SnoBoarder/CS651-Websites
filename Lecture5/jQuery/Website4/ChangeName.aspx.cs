using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AJAXJQuerySample
{
	/// <summary>
	/// Summary description for ChangeName.
	/// </summary>
	public partial class ChangeName : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Page.IsPostBack)
			{
				ProcessForm();
			}
			else
			{
				string existingName;
				if (Session["testName"] == null)
				{
					Session["testName"] = "John Smith";
				}
				existingName = Session["testName"].ToString();
				txtName.Text = existingName;
			}
		}

		private void ProcessForm()
		{
			string newName = txtName.Text;
			Session["testName"] = newName;
			Response.Clear();						//clears the existing HTML
			Response.ContentType = "text/plain";	//change content type
			Response.Write(newName);				//writes out the new name
			Response.End();							//end
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
