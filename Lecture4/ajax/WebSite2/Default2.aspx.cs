using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int nx = 0;
		int ny = 0;

		try
		{
			//Default2.aspx?nx=14&ny=23
			nx = Convert.ToInt32(Request.Params["nx"]);
			ny = Convert.ToInt32(Request.Params["ny"]);
		}
		catch (Exception ex)
		{
			nx = 0;
			ny = 0;
		}

		int answer = nx + ny;
		Response.Write(answer);
	}
}