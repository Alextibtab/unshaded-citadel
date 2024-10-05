# Define paths
$projectPath = "C:\Users\14ale\dev\rain-world\unshaded-citadel"  # Replace with your actual project path
$buildOutputPath = "$projectPath\bin\Debug\net48\unshaded-citadel.dll"
$modPath = "C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\mods\unshaded-citadel\plugins"
$gameExePath = "C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld.exe"

# Build the project
Write-Host "Building project..."
dotnet build $projectPath

# Check if build was successful
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed. Exiting script."
    exit 1
}

# Copy the DLL
Write-Host "Copying DLL to mod folder..."
Copy-Item -Path $buildOutputPath -Destination $modPath -Force

# Check if copy was successful
if (-not $?) {
    Write-Host "Failed to copy DLL. Exiting script."
    exit 1
}

# Run the game
Write-Host "Starting Rain World..."
Start-Process $gameExePath

Write-Host "Script completed successfully."
