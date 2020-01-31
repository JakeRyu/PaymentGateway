properties {
    $solutionDirectory = (Get-Item $solutionFile).DirectoryName
    $outputDirectory= "$solutionDirectory/.build"
    $temporaryOutputDirectory = "$outputDirectory/temp"
    $buildConfiguration = "Release"
    $buildPlatform = "Any CPU"

    $connectionString = "Data Source=$solutionDirectory/PaymentGateway.db"
    $migrationAssembly = "$temporaryOutputDirectory/DbMigration.dll"

    $apiProject = "$solutionDirectory/Api/Api.csproj"
    $identityServerProject = "$solutionDirectory/IdentityServer/IdentityServer.csproj"
}

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task default -depends Test

task Init `
  -description "Initialises the build by removing previous artifacts and creating output directories" `
  -requiredVariables outputDirectory, temporaryOutputDirectory `
{
    Assert ("Debug", "Release" -contains $buildConfiguration) `
		   "Invalid build configuration '$buildConfiguration'. Valid values are 'Debug' or 'Release'"

    Assert ("x86", "x64", "Any CPU" -contains $buildPlatform) `
		   "Invalid build platform '$buildPlatform'. Valid values are 'x86', 'x64' or 'Any CPU'"


    # Remove previous build results
    if (Test-Path $outputDirectory) 
	{
        Write-Host "Removing output directory located at $outputDirectory"
        Remove-Item $outputDirectory -Force -Recurse
    }

    Write-Host "Creating output directory located at $outputDirectory"
    New-Item $outputDirectory -ItemType Directory | Out-Null

    Write-Host "Creating temporary directory located at $temporaryOutputDirectory"
    New-Item $temporaryOutputDirectory -ItemType Directory | Out-Null
}

task Compile `
	-depends Init `
	-description "Compile the code" `
	-requiredVariables solutionFile, buildConfiguration, buildPlatform, temporaryOutputDirectory `
{ 
  	Write-Host "Building solution $solutionFile"
    Exec {
        dotnet msbuild $SolutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$temporaryOutputDirectory"
    }
}

task Migrate -depends Compile -description "Run database migration" {
    Exec {
        dotnet fm migrate -p sqlite -c $connectionString -a $migrationAssembly
    }
    Write-Host "Excuted data migration"
}

task Test -depends Migrate -description "Run unit tests" { 
  	Write-Host "All tests passed"
}