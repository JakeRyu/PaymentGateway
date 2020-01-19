# This triggers tasks written in Psake from default.ps1.
# Psake is a build automation tool written in PowerShell.
# On MacOS, execute below after having PowerShell installed.
# > sudo pwsh
# > Install-Module psake

cls

remove-module [p]sake

Import-Module psake

Invoke-psake


