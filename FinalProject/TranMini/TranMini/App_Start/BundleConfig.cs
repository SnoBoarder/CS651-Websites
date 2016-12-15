using System.Web;
using System.Web.Optimization;

namespace TranMini
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery-{version}.js")
				.Include("~/Scripts/jquery.cookie.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/signalr")
				.Include("~/Scripts/jquery.signalR-{version}.js")
				.Include("~/signalr/hubs"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new ScriptBundle("~/bundles/gamejs")
				.Include("~/Scripts/endgate-{version}.js")
				.Include("~/Scripts/p2.js")
				.Include("~/Scripts/pixi.js")
				.Include("~/Scripts/phaser.js")
				.Include("~/GameClient/IClientInitialization.js")
				.Include("~/GameClient/IConfigurationDefinition.js")
				.Include("~/GameClient/IPayloadDefinition.js")
				.Include("~/GameClient/IUserInformation.js")
				.Include("~/GameClient/UtilityFunction.js")
				.Include("~/GameClient/LatencyResolver.js")
				.Include("~/GameClient/ServerConnectionManager.js")
				.Include("~/GameClient/ServerAdapter.js")
				.Include("~/GameClient/Square.js")
				.Include("~/GameClient/PayloadDecompressor.js")
				.Include("~/GameClient/SquareGraphic.js")
				.Include("~/GameClient/UserSquareManager.js")
				.Include("~/GameClient/SquareManager.js")
				.Include("~/GameClient/Game.js")
				.Include("~/GameClient/ConfigurationManager.js")
				.Include("~/GameClient/Main.js"));
		}
	}
}
