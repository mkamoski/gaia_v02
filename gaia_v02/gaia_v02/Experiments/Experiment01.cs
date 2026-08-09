using gaia_v02.Domain;

namespace gaia_v02.Experiments;

/// <summary>
/// Experiment01 – Minimal phase-space pipeline proof-of-concept.
///
/// Steps:
///   1. Generate synthetic Gaia-like stellar phase-space data.
///   2. Partition stars into spatial cells near the galactic midplane.
///   3. Compute velocity dispersions (σR, σZ) per cell and the ratio σR/σZ.
///   4. Flag cells that match the physically expected ratio (~1.93).
///   5. Write per-cell results to a CSV file next to the EXE.
///
/// A CancellationToken with a 5-minute timeout is honoured throughout.
/// </summary>
public sealed class Experiment01
{
    // Physical constants / simulation parameters
    private const int StarCount = 50_000;
    private const double R0 = 8.0;          // Solar galactocentric radius [kpc]
    private const double SigmaR = 38.0;     // Radial velocity dispersion [km/s]
    private const double SigmaZ = 20.0;     // Vertical velocity dispersion [km/s] → ratio ≈ 1.9
    private const double SigmaPhi = 28.0;   // Azimuthal velocity dispersion [km/s]
    private const double V_LSR = 220.0;     // Local standard of rest [km/s]
    private const double ScaleHeightR = 2.5; // Radial scale length [kpc]
    private const double ScaleHeightZ = 0.3; // Vertical scale height [kpc]
    private const double ExpectedRatio = 1.93;
    private const double RatioTolerance = 0.15;

    // Grid cell sizes for spatial binning
    private const double CellDeltaR = 0.5;  // [kpc]
    private const double CellDeltaZ = 0.15; // [kpc]

    public async Task<ExperimentResult> RunAsync(CancellationToken cancellationToken)
    {
        var lines = new List<string>();
        var log = new List<string>();

        log.Add($"[Experiment01] Started at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        log.Add($"[Experiment01] Generating {StarCount:N0} synthetic stars...");

        await Task.Yield(); // allow UI to update

        // --- Step 1: Generate synthetic stars ---
        cancellationToken.ThrowIfCancellationRequested();
        var stars = GenerateStars(cancellationToken);
        log.Add($"[Experiment01] Generated {stars.Count:N0} stars.");

        // --- Step 2: Bin into spatial cells ---
        cancellationToken.ThrowIfCancellationRequested();
        var cells = BinIntoCells(stars, cancellationToken);
        log.Add($"[Experiment01] Identified {cells.Count} spatial cells.");

        // --- Step 3 & 4: Compute dispersions and evaluate ratio ---
        cancellationToken.ThrowIfCancellationRequested();
        var cellResults = ComputeDispersions(cells, cancellationToken);
        int passCount = cellResults.Count(c => c.PassesRatioCheck);
        log.Add($"[Experiment01] Cells passing σR/σZ ratio check: {passCount} / {cellResults.Count}");

        // --- Step 5: Build CSV ---
        lines.Add("CellR_kpc,CellZ_kpc,StarCount,SigmaR_kms,SigmaZ_kms,Ratio_SigmaR_SigmaZ,ExpectedRatio,PassesCheck");
        foreach (var c in cellResults)
        {
            cancellationToken.ThrowIfCancellationRequested();
            lines.Add(
                $"{c.CellR:F2},{c.CellZ:F3},{c.N}," +
                $"{c.SigmaR:F4},{c.SigmaZ:F4},{c.Ratio:F4}," +
                $"{ExpectedRatio:F2},{(c.PassesRatioCheck ? "YES" : "NO")}");
        }

        log.Add($"[Experiment01] Finished at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        return new ExperimentResult(
            CsvLines: lines,
            LogLines: log,
            CellResults: cellResults);
    }

    // -----------------------------------------------------------------------
    private List<StarPhaseSpace> GenerateStars(CancellationToken ct)
    {
        var rng = new Random(42);
        var stars = new List<StarPhaseSpace>(StarCount);

        for (int i = 0; i < StarCount; i++)
        {
            if (i % 5000 == 0) ct.ThrowIfCancellationRequested();

            // Position: exponential disk profile sampled via inverse CDF approximation
            double r = R0 + SampleExponential(rng, ScaleHeightR) - ScaleHeightR;
            r = Math.Max(0.5, r);
            double z = SampleLaplace(rng, ScaleHeightZ);

            // Velocities: Gaussian dispersions around LSR
            double vr   = SampleGaussian(rng, 0.0, SigmaR);
            double vz   = SampleGaussian(rng, 0.0, SigmaZ);
            double vphi = SampleGaussian(rng, V_LSR, SigmaPhi);

            stars.Add(new StarPhaseSpace(r, z, vr, vz, vphi));
        }

        return stars;
    }

    private Dictionary<(int iR, int iZ), List<StarPhaseSpace>> BinIntoCells(
        List<StarPhaseSpace> stars, CancellationToken ct)
    {
        var cells = new Dictionary<(int, int), List<StarPhaseSpace>>();

        foreach (var s in stars)
        {
            ct.ThrowIfCancellationRequested();
            int iR = (int)Math.Floor(s.R / CellDeltaR);
            int iZ = (int)Math.Floor(s.Z / CellDeltaZ);
            if (!cells.TryGetValue((iR, iZ), out var list))
            {
                list = [];
                cells[(iR, iZ)] = list;
            }
            list.Add(s);
        }

        return cells;
    }

    private List<CellResult> ComputeDispersions(
        Dictionary<(int iR, int iZ), List<StarPhaseSpace>> cells,
        CancellationToken ct)
    {
        var results = new List<CellResult>(cells.Count);
        const int minStars = 20;

        foreach (var kvp in cells)
        {
            ct.ThrowIfCancellationRequested();
            var list = kvp.Value;
            if (list.Count < minStars) continue;

            double cellR = (kvp.Key.iR + 0.5) * CellDeltaR;
            double cellZ = (kvp.Key.iZ + 0.5) * CellDeltaZ;

            double sigR = StandardDeviation(list, s => s.VR);
            double sigZ = StandardDeviation(list, s => s.VZ);

            double ratio = sigZ > 0.0 ? sigR / sigZ : double.NaN;
            bool passes = !double.IsNaN(ratio) &&
                          Math.Abs(ratio - ExpectedRatio) <= RatioTolerance;

            results.Add(new CellResult(cellR, cellZ, list.Count, sigR, sigZ, ratio, passes));
        }

        results.Sort((a, b) =>
        {
            int c = a.CellR.CompareTo(b.CellR);
            return c != 0 ? c : a.CellZ.CompareTo(b.CellZ);
        });

        return results;
    }

    // -----------------------------------------------------------------------
    // Sampling helpers

    private static double SampleGaussian(Random rng, double mean, double sigma)
    {
        // Box-Muller
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        return mean + sigma * z;
    }

    private static double SampleExponential(Random rng, double scale)
        => -scale * Math.Log(1.0 - rng.NextDouble());

    private static double SampleLaplace(Random rng, double scale)
    {
        double u = rng.NextDouble() - 0.5;
        return -scale * Math.Sign(u) * Math.Log(1.0 - 2.0 * Math.Abs(u));
    }

    private static double StandardDeviation(List<StarPhaseSpace> list, Func<StarPhaseSpace, double> selector)
    {
        double mean = list.Average(selector);
        double variance = list.Sum(s => Math.Pow(selector(s) - mean, 2)) / list.Count;
        return Math.Sqrt(variance);
    }
}

// -----------------------------------------------------------------------

public sealed record CellResult(
    double CellR,
    double CellZ,
    int N,
    double SigmaR,
    double SigmaZ,
    double Ratio,
    bool PassesRatioCheck);

public sealed record ExperimentResult(
    IReadOnlyList<string> CsvLines,
    IReadOnlyList<string> LogLines,
    IReadOnlyList<CellResult> CellResults);
