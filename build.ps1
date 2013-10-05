# helper script for building the project using psake.
param(
    [Parameter(Position=0, Mandatory=0)]
    [string[]]$task_list = @()
)

$root       = Resolve-Path .
$nuget_path = "$root\src\.nuget"
$psake_path = "$root\packages\psake.4.2.0.1\tools"

# restore psake if it's missing.
& $nuget_path\NuGet.exe restore $nuget_path\packages.config -Verbosity quiet -NonInteractive -ConfigFile $nuget_path\NuGet.config

& $psake_path\psake.ps1 default.ps1 $task_list
