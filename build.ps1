# Psake is a build automation tool written in PowerShell.
# In order to use Psake on MacOS, install PowerShell then, run commnd lines as below.
# > sudo pwsh
# > Install-Module psake
# On Windows, follow the instruction at https://github.com/psake/psake.

remove-module [p]sake

Import-Module psake 

Invoke-psake -buildFile default.ps1 `
			 -framework netcoreapp3.0 `
		     -properties @{ 
				 "buildConfiguration" = "Release"
				 "buildPlatform" = "Any CPU"} `
			 -parameters @{ 
				 "solutionFile" = "PaymentGateway.sln"}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode


