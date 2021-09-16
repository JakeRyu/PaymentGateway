remove-module [p]sake

nuget install psake -Version 4.9.0 -OutputDirectory Packages

Import-Module "./Packages/psake.4.9.0/tools/psake/psake.psm1"

Invoke-psake -buildFile default.ps1 `
			 -framework netcoreapp3.0 `
		     -properties @{ 
				 "buildConfiguration" = "Release"
				 "buildPlatform" = "Any CPU"} `
			 -parameters @{ 
				 "solutionFile" = "../PaymentGateway.sln"
				 "framework" = "netcoreapp3.0"}

Write-Host "Build exit code:" $LastExitCode

exit $LastExitCode


