How to run project:
- Open "TranMini.sln"
- Run the project, which will setup a virtual IIS Express service.
- Enjoy!

Online example:
- http://tranmini.azurewebsites.net/Home/Game
- NOTE: If it's not running, please notify me and I will fix it: btran89@bu.edu

If you're missing packages:
	The following packages need to be installed through Package Manager Console:
	Id                                                 Versions     ProjectName
	--                                                 --------     -----------
	Antlr                                              {3.4.1.9004} TranMini   
	bootstrap                                          {3.0.0}      TranMini   
	jQuery                                             {1.10.2}     TranMini   
	jQuery.Cookie                                      {1.4.1}      TranMini   
	jquery.cookie.TypeScript.DefinitelyTyped           {0.2.1}      TranMini   
	jquery.TypeScript.DefinitelyTyped                  {3.1.2}      TranMini   
	jQuery.Validation                                  {1.11.1}     TranMini   
	Microsoft.AspNet.Mvc                               {5.2.3}      TranMini   
	Microsoft.AspNet.Razor                             {3.2.3}      TranMini   
	Microsoft.AspNet.SignalR                           {2.2.1}      TranMini   
	Microsoft.AspNet.SignalR.Core                      {2.2.1}      TranMini   
	Microsoft.AspNet.SignalR.JS                        {2.2.1}      TranMini   
	Microsoft.AspNet.SignalR.SystemWeb                 {2.2.1}      TranMini   
	Microsoft.AspNet.Web.Optimization                  {1.1.3}      TranMini   
	Microsoft.AspNet.WebPages                          {3.2.3}      TranMini   
	Microsoft.CodeDom.Providers.DotNetCompilerPlatform {1.0.0}      TranMini   
	Microsoft.jQuery.Unobtrusive.Validation            {3.2.3}      TranMini   
	Microsoft.Net.Compilers                            {1.0.0}      TranMini   
	Microsoft.Owin                                     {3.0.1}      TranMini   
	Microsoft.Owin.Host.SystemWeb                      {2.1.0}      TranMini   
	Microsoft.Owin.Security                            {3.0.1}      TranMini   
	Microsoft.Owin.Security.Cookies                    {3.0.1}      TranMini   
	Microsoft.Web.Infrastructure                       {1.0.0.0}    TranMini   
	Modernizr                                          {2.6.2}      TranMini   
	Newtonsoft.Json                                    {6.0.4}      TranMini   
	Owin                                               {1.0}        TranMini   
	Respond                                            {1.2.0}      TranMini   
	signalr.TypeScript.DefinitelyTyped                 {0.4.1}      TranMini   
	WebGrease                                          {1.5.2}      TranMini   

Hiccups:
- BundleConfig
	- The BundleConfig needs to be setup to compile all typescript files properly
- TypeScript
	- Syntax!
- Manipulated "jumping" data before it was sent out to everyone!
- Authentication cookie
- Registration
- Negotiation
- HighFrequencyTimer
- GameTime
https://github.com/NTaylorMullen/ShootR/tree/master/ShootR