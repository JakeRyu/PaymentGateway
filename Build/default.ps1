task default -depends Test

task Clean {
    Write-Host 'Executed Clean!'
}

task Compile -depends Clean {
    Write-Host 'Executed Compile!'
}

task Test -depends Compile, Clean {
    Write-Host 'Executed Test!'
}