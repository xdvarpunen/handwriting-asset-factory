open System
open System.IO
open System.Windows
open System.Windows.Controls
open System.Windows.Forms
open System.Windows.Forms.Integration
open System.Windows.Input
open System.Windows.Media
open System.Windows.Media.Imaging

module Program =

    type MainForm(csvFilename: string, width: int, height: int, pngFilename: string) as this =
        inherit Form()

        let elementHost = new ElementHost()
        let inkCanvas = new InkCanvas(Background = Brushes.White)

        do
            this.Text <- csvFilename // Set the window title to only the CSV filename
            this.Width <- width
            this.Height <- height
            this.StartPosition <- FormStartPosition.CenterScreen
            this.MaximizeBox <- false

            // Set up the ElementHost and InkCanvas
            elementHost.Child <- inkCanvas :> System.Windows.UIElement
            elementHost.Dock <- DockStyle.Fill
            this.Controls.Add(elementHost)

            // Load background image if it exists
            if File.Exists(pngFilename) then
                let imageBrush = new ImageBrush()
                imageBrush.ImageSource <- new BitmapImage(new Uri(pngFilename, UriKind.RelativeOrAbsolute))
                imageBrush.Stretch <- Stretch.Fill
                inkCanvas.Background <- imageBrush

            // Enable key preview so the form captures key events
            this.KeyPreview <- true

            // Form closing event
            this.FormClosing.Add(fun _ ->
                this.saveInkCanvasToCSV csvFilename
            )

            // Focus the form on shown event
            this.Shown.Add(fun _ -> this.Focus() |> ignore) // Ignoring the return value

        member private this.saveInkCanvasToCSV(filePath: string) =
            try
                let strokeCollection = inkCanvas.Strokes
                use writer = new StreamWriter(filePath)

                writer.WriteLine("StrokeNumber;X;Y")

                for strokeIndex in 0 .. strokeCollection.Count - 1 do
                    let stroke = strokeCollection.[strokeIndex]
                    for point in stroke.StylusPoints do
                        let strokeNumber = strokeIndex + 1
                        let x = point.X.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                        let y = point.Y.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                        writer.WriteLine("{0};{1};{2}", strokeNumber, x, y)

                writer.Flush()
            with
            | ex -> MessageBox.Show($"Failed to save CSV file: {ex.Message}") |> ignore

    [<STAThread>]
    [<EntryPoint>]
    let main argv =
        if argv.Length < 4 then
            MessageBox.Show("Error: Please provide a filename, width, height, and PNG filename.\nUsage: Program <filename> <width> <height> <pngFilename>", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
            1
        else
            let csvFilename = argv.[0]
            let widthStr = argv.[1].Trim()
            let heightStr = argv.[2].Trim()
            let pngFilename = argv.[3]

            try
                let width = Int32.Parse(widthStr)
                let height = Int32.Parse(heightStr)

                Application.EnableVisualStyles()
                Application.SetCompatibleTextRenderingDefault(false)
                Application.Run(new MainForm(csvFilename, width, height, pngFilename))

                0
            with
            | :? FormatException ->
                MessageBox.Show($"Error: Width and Height must be valid integers.\nProvided Width: '{widthStr}'\nProvided Height: '{heightStr}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
                1
            | ex ->
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
                1
