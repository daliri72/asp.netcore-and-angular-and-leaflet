# asp.netcore-and-angular-and-leaflet
Leaflet is the leading open-source JavaScript library for mobile-friendly interactive maps. Weighing just about 38 KB of JS, it has all the mapping features most developers ever need.
A simple app with Asp.net core 2.1 and Angular 8



![jwtauth](/backedn/src/ASPNETCore2JwtAuthentication.WebApp/wwwroot/images/location.PNG)


- A separated EF Core data layer with enabled migrations.
- An EF Core 2.2.102 based service layer.


How to run the Angular 7.0+ Client
-------------

- Update all of the outdated global dependencies using the `npm update -g` command.
- Install the `Angular-CLI`.
- Open a command prompt console and then navigate to src/ASPNETCore2JwtAuthentication.AngularClient/ folder.
- Now run the following commands:

```PowerShell
npm update -g
npm install
```

- Then open another command prompt console and navigate to backend/src/ASPNETCore2JwtAuthentication.WebApp/ folder.
- Now run the following commands:

```PowerShell
dotnet restore
dotnet watch run
```