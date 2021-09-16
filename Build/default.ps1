properties {
    $solutionDirectory = (Get-Item $solutionFile).DirectoryName
    $buildConfiguration = "Release"
    $buildPlatform = "Any CPU"

    $connectionString = "Data Source=$solutionDirectory/PaymentGateway.db"
    $migrationAssembly = "$solutionDirectory/DbMigration/bin/$buildConfiguration/$framework/DbMigration.dll"

    $apiProject = "$solutionDirectory/Api/Api.csproj"
    $identityServerProject = "$solutionDirectory/IdentityServer/IdentityServer.csproj"
    $applicationUnitTestsProject = "$solutionDirectory/Application.UnitTests/Application.UnitTests"
    $domainUnitTestsProject = "$solutionDirectory/Domain.UnitTests/Domain.UnitTests"
}

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task default -depends Api

task Compile `
	-description "Compile the code" `
	-requiredVariables solutionFile, buildConfiguration, buildPlatform `
{ 
  	Write-Host "Building solution $solutionFile"
    Exec {
        dotnet msbuild $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform"
    }
}

task Migrate -depends Compile `
    -description "Run database migration" `
    -requiredVariable connectionString, migrationAssembly `
{
    Exec {
        dotnet fm migrate -p sqlite -c $connectionString -a $migrationAssembly
    }
    Write-Host "Excuted data migration"
}

task Test -depends Migrate `
    -description "Run unit tests" `
    -requiredVariable solutionFile `
{ 
    Exec {
            dotnet test $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform" --no-build
    }   
  	Write-Host "All tests passed"
}

task Api -depends Test `
    -description "Run API" `
    -requiredVariable apiProject `
{
    Write-Host "Running API..."
    Exec {
        dotnet run --no-build --project $apiProject --configuration $buildConfiguration
    }
}