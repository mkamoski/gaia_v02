using gaia_v02.Experiments;

namespace gaia_v02;

public partial class Form1 : Form
{
    private static readonly TimeSpan ExperimentTimeout = TimeSpan.FromMinutes(5);

    public Form1()
    {
        InitializeComponent();
    }

    private async void BtnRunExperiment01_Click(object sender, EventArgs e)
    {
        btnRunExperiment01.Enabled = false;
        txtOutput.Clear();
        lblStatus.Text = "Running Experiment 01… (timeout 5 min)";

        using var cts = new CancellationTokenSource(ExperimentTimeout);

        try
        {
            var experiment = new Experiment01();
            var result = await Task.Run(() => experiment.RunAsync(cts.Token), cts.Token);

            // Write CSV next to EXE
            string csvPath = Path.Combine(
                AppContext.BaseDirectory,
                $"Experiment01_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

            await File.WriteAllLinesAsync(csvPath, result.CsvLines, cts.Token);

            // Build output text
            var sb = new System.Text.StringBuilder();
            foreach (var line in result.LogLines)
                sb.AppendLine(line);

            sb.AppendLine();
            sb.AppendLine($"CSV written to: {csvPath}");
            sb.AppendLine();

            // Summary table header
            sb.AppendLine($"{"CellR",8} {"CellZ",8} {"N",6} {"σR",8} {"σZ",8} {"σR/σZ",8} {"Pass?",6}");
            sb.AppendLine(new string('-', 60));

            foreach (var c in result.CellResults)
            {
                sb.AppendLine(
                    $"{c.CellR,8:F2} {c.CellZ,8:F3} {c.N,6} " +
                    $"{c.SigmaR,8:F2} {c.SigmaZ,8:F2} {c.Ratio,8:F3} " +
                    $"{(c.PassesRatioCheck ? "YES" : "no"),6}");
            }

            txtOutput.Text = sb.ToString();
            lblStatus.Text = $"Done. Results saved to {Path.GetFileName(csvPath)}";
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text = "Experiment 01 timed out after 5 minutes.";
            txtOutput.AppendText("\r\n[TIMEOUT] Experiment was cancelled after 5 minutes.");
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error – see output.";
            txtOutput.AppendText($"\r\n[ERROR] {ex.Message}\r\n{ex.StackTrace}");
        }
        finally
        {
            btnRunExperiment01.Enabled = true;
        }
    }
}

