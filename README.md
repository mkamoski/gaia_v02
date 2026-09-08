gaia_v02 — Phase-space toy experiment (Gaia-like)
=================================================

Project summary
---------------
gaia_v02 is a minimal, Windows Forms proof-of-concept that generates synthetic, Gaia-like stellar phase-space samples in cylindrical galactic coordinates and analyses local kinematic dispersions. The experiment partitions stars into spatial cells (R, Z) near the midplane, computes velocity dispersions in each cell (σR and σZ), and flags cells whose σR/σZ ratio matches an expected physical value.

This repository is intended as a small research/teaching/demo app to show sampling, simple spatial binning, and dispersion statistics on synthetic data.

Key files
---------
- gaia_v02/Experiments/Experiment01.cs — core experiment pipeline (generation, binning, dispersion calculation, CSV output).
- gaia_v02/Domain/StarPhaseSpace.cs — immutable record type for a single star's phase-space coordinates (R, Z, VR, VZ, VPhi).
- gaia_v02/Form1.cs and Form1.Designer.cs — WinForms UI that exposes parameters and runs the experiment, writes CSV next to the EXE.
- gaia_v02/Program.cs — WinForms program entry point.

High-level algorithm (Experiment01)
-----------------------------------
1. Generate N synthetic stars (GenerateStars):
   - R (galactocentric radius) is sampled as R0 + Exponential(scale=ScaleHeightR) − ScaleHeightR, floored to a minimum (0.5 kpc in code).
   - Z (height above plane) is sampled from a Laplace distribution with scale = ScaleHeightZ.
   - Velocities VR, VZ, and VPhi are sampled from Gaussians (normal distributions). VPhi is centered on V_LSR.

2. Partition the stars into rectangular spatial cells in (R, Z): index iR = floor(R / CellDeltaR) and iZ = floor(Z / CellDeltaZ). Each unique (iR, iZ) defines a cell.

3. For each cell that has at least MinStarsPerCell stars, compute:
   - σR = Standard deviation of VR over the cell.
   - σZ = Standard deviation of VZ over the cell.
   - Ratio = σR / σZ (NaN if σZ == 0).
   - Passes check if |Ratio − ExpectedRatio| ≤ RatioTolerance.

4. Save per-cell results into a CSV file and present a summary in the UI.

Sampling and math details
-------------------------
- Gaussian (normal) sampling: the implementation uses Box–Muller transform. Given two independent uniform(0,1) samples u1,u2, a standard normal variate z is computed by

  z = sqrt(-2 ln u1) * cos(2π u2)

  The code returns mean + sigma * z.

- Exponential sampling (inverse CDF): for scale λ (mean = scale), sample u ~ Uniform(0,1) and return -scale * ln(1 − u).

- Laplace sampling (double-exponential): the code uses the inverse CDF approach. For u ~ Uniform(0,1) shifted to (-0.5, +0.5):

  x = -scale * sign(u) * ln(1 − 2|u|)

- Standard deviation calculation: the pipeline computes the (population) standard deviation per cell using

  mean = (1/N) ∑ xi
  variance = (1/N) ∑ (xi − mean)^2
  σ = sqrt(variance)

  Note: this uses the population denominator N (not N−1). This is consistent across cells in the current code.

CSV output
----------
Each output CSV row contains the following columns:
- CellR_kpc — center radius of the cell (kpc)
- CellZ_kpc — center height of the cell (kpc)
- StarCount — number of stars in the cell
- SigmaR_kms — σR (km/s)
- SigmaZ_kms — σZ (km/s)
- Ratio_SigmaR_SigmaZ — σR/σZ (or NaN)
- ExpectedRatio — the target ratio provided by parameters
- PassesCheck — YES/NO whether the ratio is within tolerance

Default parameters (from Experiment01Parameters.Default)
-----------------------------------------------------
- StarCount: 50,000
- R0 (Solar radius): 8.0 kpc
- SigmaR: 38.0 km/s
- SigmaZ: 20.0 km/s
- SigmaPhi: 28.0 km/s
- V_LSR: 220.0 km/s
- ScaleHeightR: 2.5 kpc
- ScaleHeightZ: 0.3 kpc
- ExpectedRatio: 1.93
- RatioTolerance: 0.15
- CellDeltaR: 0.50 kpc
- CellDeltaZ: 0.15 kpc
- MinStarsPerCell: 20
- TimeoutMinutes: 5

Computational complexity & performance notes
-------------------------------------------
- Generating N stars is O(N) in time and O(N) memory for the in-memory list.
- Binning into cells is O(N) average-case (dictionary insertion per star). The number of distinct cells M is bounded by the spatial range and cell size; memory usage for the dictionary is O(N) in the worst case.
- Computing dispersions iterates only over populated cells and computes averages and sums over each cell's stars — total cost is O(N) across all cells.
- The default RNG seed (42) is fixed in code for deterministic reproducibility. If you want stochastic runs, seed with a variable (e.g., time-based seed).

How to build and run
--------------------
1. Open the solution gaia_v02.slnx in Visual Studio 2022/2024/2026 (project targets .NET 10).
2. Build and run the Windows Forms app. The UI exposes all experiment parameters and provides a Run button. The experiment will write a CSV next to the EXE (AppContext.BaseDirectory) with the timestamped filename Experiment01_YYYYMMDD_HHMMSS.csv.

Notes and potential improvements
-------------------------------
- Consider using streaming / chunked processing to reduce peak memory if StarCount is extremely large.
- If you want unbiased sample standard deviations for small N, use the sample variance (divide by N−1) instead of N.
- Add parallelism: generation and binning are mostly embarrassingly parallel and could be parallelized (careful with RNG concurrency and dictionary concurrency).
- Consider using more realistic galactic kinematic models (non-gaussian tails, radial dependence in dispersions, or correlations between velocity components) for advanced studies.

Files / symbols to inspect for customization
-------------------------------------------
- Experiment01.GenerateStars — change sampling distributions and seed.
- Experiment01.BinIntoCells — change cell geometry (hex/overlapping windows) or indexing.
- Experiment01.ComputeDispersions — change statistic (robust estimators, median absolute deviation, bootstrapped errors).

License / attribution
---------------------
This repository is provided as-is for demonstration and research. There is no embedded third-party data; generated data is synthetic.

Contact
-------
For code questions, inspect Experiment01.cs and Form1.* in the gaia_v02 project.
