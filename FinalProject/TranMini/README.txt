Write all packages to install:
- Install-Package jquery typescript
- Install-Package jquery cookie typescript
- Install-Package signalR typescript
- Install-Package jquery.cookie
- Install-Package Microsoft.Owin.Security.Cookies

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
	