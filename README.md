# handwriting-asset-factory

Handwriting assets creation app made in Windows Form & WPF. Produces with given parameters in CLI a window where can draw and saves to csv.

- Ink Canvas: The application includes an InkCanvas that allows users to draw freely with a stylus or mouse. This is useful for applications requiring sketching or freehand drawing.
- Background Image Support: Users can load a background image into the InkCanvas from a specified PNG file. This can be helpful for tracing or annotating existing images.
- CSV Export: The application can save the drawn strokes and their coordinates (X, Y) to a CSV file. This includes stroke number, enabling users to analyze or process the drawn data further.
- Form Customization: The application window can be customized with a title based on the provided CSV filename, width, and height. It opens centered on the screen and has a fixed size.

## Get Started

```powershell
dotnet restore
dotnet build --configuration Release
cd AssetFactory\bin\Release\net8.0-windows
# AssetFactory.exe should be available for use
```

## Usage

```powershell
./AssetFactory.exe "letter_x.csv" 400 400 "background.png"
```

![image](https://github.com/user-attachments/assets/0621bb9f-5a93-4e20-a479-db5e5a0f79e3)


or

```powershell
# Use/modify letters.ps1 to produce multiple csv files.
./letters.ps1
```

