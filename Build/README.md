# Build

Use Psake to build the solution and run test projects. Install Psake NuGet package inside the project to make it easy to import Psake in the PowerShell script.

Open PowerShell as administrator as writing permission is needed. For MacOS, run 
```
sudo pwsh
```

Change directory to `Build` and run 
```
./build.ps1
``` 

It invokes,

1. Build the entire solution
2. Migrate database
3. Run test projects
4. If the tests pass, run API

The API is listening on http://localhost:5000, https://localhost:5001

![api-swagger](../Documents/api-swagger.png)