param(
	[Parameter(Mandatory=$true)] [string] $ArtifactsDir,
	[Parameter(Mandatory=$true)] [string] $PublishDir,
	[Parameter(Mandatory=$true)] [string] $ProjectDir,
	[Parameter(Mandatory=$true)] [string] $Configuration,
	[Parameter(Mandatory=$true)] [string] $TargetFramework
)

try {
	$timestamp = Get-Date -Format 'yyyyMMdd_HHmmss'
	$temp = Join-Path $ArtifactsDir ("tempzip_$timestamp")
	if (Test-Path $temp) { Remove-Item $temp -Recurse -Force }
	New-Item -ItemType Directory -Path $temp | Out-Null

	# Copy source (exclude common build/artifact folders)
	robocopy "$ProjectDir" "$temp\source" /MIR /XD bin obj .git artifacts .vs /XF *.user *.suo *.cache *.zip | Out-Null

	# Copy published output
	if (Test-Path $PublishDir) {
		robocopy "$PublishDir" "$temp\publish" /MIR | Out-Null
	}

	$zipName = "Gaia_v02_${Configuration}_${TargetFramework}_$timestamp.zip"
	$zip = Join-Path $ArtifactsDir $zipName
	if (Test-Path $zip) { Remove-Item $zip -Force }

	Compress-Archive -Path (Join-Path $temp '*') -DestinationPath $zip -Force

	Remove-Item $temp -Recurse -Force
	Write-Host "Created ZIP: $zip"
	exit 0
}
catch {
	Write-Error "Failed to create artifact zip: $_"
	exit 1
}
