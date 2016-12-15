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

http://tranmini.azurewebsites.net/Home/Game

Things I used:
- Azure
	- easily published my website through visual studio
- SignalR
- Phaser
- TypeScript
- .NET MVC Framework 4.6
- egate

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

PayloadDecompressor
ServerConnectionManager
LatencyResolver
IUserInformation
IPayloadDefinition
IConfigurationDefinition
IClientInitialization
ConfigurationManager
ServerConnectionManager
IConfigurationDefinition
Square

TODO:
- Have a counter above each player representing successful jumps over the box.
	- Just update the score for everyone once the box reaches the end of the screen!
	- Put it in the square class and not have the Y be affected!
	- Keep track of high score
- Reset functionality
- Make enemy smoother
	- Let the enemy bounce on the bottom
	- Generate another sprite and tween it at the same rate as what we want the server one to be. once they're synced, then make sure the algorithm is setup properly to sync them
- get sprite assets :D
- Get title (just use font and write "Tran Mini Game!")
- Presentation:
	- Introduction
		- Wanted to make a game
		- Initial ambition
	- Tech
		- .NET Framework 4.6 MVC
			- OWIN
		- SignalR
		- TypeScript
		- Phaser
		- egate (purely for its event handling)
	- Tools
		- Azure (for hosting)
		- GitHub
		- Visual Studio 2015
		- Chrome Developer Tools
	- Client Server Relationship
		- Show diagram!
	- Payloads
		- Explain why i made this
	- Collision Detection
		- Basic bounding boxes colliding
	- Do more on the client!
		- animations! time based activity
	