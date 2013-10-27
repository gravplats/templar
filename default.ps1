include .\extensions.ps1

properties {
    # name of solution and nuspec.
    $name                   = "Templar"
    $version                = "0.1"

    # files that should be part of the nuget.
    $nuget_package_files    = @( "$name.???" )
    
    # shouldn't normally be any need change the variables below this comment.
    $root                   = Resolve-Path .
    $root_parent            = Resolve-Path "$root\.."

    # build
	$build_path             = "$root\build"
	$build_binaries_path    = "$build_path\binaries\"
    $build_nuget_path       = "$build_path\nuget"    
   
    # source
	$source_path            = "$root\src"
    $solution_file          = "$source_path\$name.sln"
    
    # nuget
    $nuspec                 = "$name.nuspec"
    $nuspec_build_file      = "$build_path\nuget\$nuspec"
    $nuspec_file            = "$root\$nuspec"

    $nuget_executable       = "$source_path\.nuget\NuGet.exe"
    $nuget_package          = "$build_nuget_path\$name.$version.nupkg"
}

Framework "4.0"

task Default -depends build

task clean {
    Remove-Item -Force -Recurse -ErrorAction SilentlyContinue -LiteralPath $build_path
}

task build -depends clean {
    # read metadata from nuspec and update AssemblyInfo.
    $metadata = ([xml](Get-Content $nuspec_file)).package.metadata
    
    # the AssemblyVersionAttribute is no fan of beta versions and will fail the build, thus
    # we will be using the syntax major[.minor[.patch]].beta-version in AssemblyInfo.
    $assembly_version = $version -replace '(\d+\\.)?(\d+\\.)?(\d+)-beta(\d+)', '$1$2$3.$4'
    
    Generate-Assembly-Info `
        -file "$source_path\AssemblyInfo.cs" `
        -title $metadata.id + " $version" `
        -description $metadata.description `
        -product $metadata.id `
        -version $assembly_version `
        -copyright "Copyright (c) Mattias Rydengren 2013"

    New-Item $build_binaries_path -ItemType Directory | Out-Null
    exec { msbuild $solution_file /p:OutDir=$build_binaries_path /p:Configuration=Release /v:q }
}

task test -depends build {
    $test_runner_executable = Resolve-Path "$root\packages\NUnit.Runners.*\tools\nunit-console-x86.exe"
    $published_websites_path = "$build_binaries_path\_PublishedWebsites"
    
    if (Test-Path $published_websites_path) {
        # make sure tests find web project, if needed.
        Move-Item $published_websites_path\$name.Web $build_binaries_path
        Remove-Item $published_websites_path       
    }
    
    Get-Item $build_binaries_path\*Tests*.dll |% {
        exec { & $test_runner_executable $_ /noresult }
    }
}

task nuget -depends build {
    # copy files that should be part of the nuget.
    New-Item $build_nuget_path\lib\net40 -ItemType Directory | Out-Null
    $nuget_package_files |% { Copy-Item $build_binaries_path\$_ $build_nuget_path\lib\net40 }

    # make a copy of the nuspec making temporary edits possible.
    Copy-Item $nuspec_file $nuspec_build_file
    
	# update version in the nuspec copy.
    $content = [xml](Get-Content $nuspec_build_file)
	$content.package.metadata.version = $version
	$content.Save($nuspec_build_file)
    
    exec { & $nuget_executable pack $nuspec_build_file -o $build_nuget_path }
}
