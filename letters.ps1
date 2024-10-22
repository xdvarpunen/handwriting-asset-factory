# Path to your executable
$exePath = ".\AssetFactory.exe"
# Create an array of letters a-z
$letters = @('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z')

# Loop through letters a-z
foreach ($letter in $letters) {
 
    # Create CSV filename
    $csvFilename = ".\$letter.csv"

    # Define width, height, and PNG filename (if any)
    $width = "800"
    $height = "600"
    $pngFilename = "output.png" # Change as needed

    # Start the process
    $process = Start-Process -FilePath $exePath -ArgumentList "$csvFilename", $width, $height, $pngFilename -PassThru

    # Wait for the process to exit
    $process.WaitForExit()
}
