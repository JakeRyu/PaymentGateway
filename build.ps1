# Psake is a build automation tool written in PowerShell.
# In order to use Psake on MacOS, install PowerShell then, run commnd lines as below.
# > sudo pwsh
# > Install-Module psake
# On Windows, follow the instruction at https://github.com/psake/psake.

remove-module [p]sake

Import-Module psake

# Default parameter for Invoke-psake command is default.ps1
Invoke-psake


