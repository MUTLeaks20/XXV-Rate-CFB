using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace JsonDataDisplay
{
    public partial class PlayerStatsEntryWindow : Window
    {
        // Properties to store entered stat values
        public int SpeedWeight { get; private set; }
        public int AgilityWeight { get; private set; }
        public int AccelerationWeight { get; private set; }
        public int StrengthWeight { get; private set; }
        public int AwarenessWeight { get; private set; }
        public int ThrowPowerWeight { get; private set; }
        public int ThrowAccuracyWeight { get; private set; }
        public int CatchInTrafficWeight { get; private set; }
        public int TackleWeight { get; private set; }
        public int BreakTackleWeight { get; private set; }
        public int KickPowerWeight { get; private set; }
        public int BallCarrierVisionWeight { get; private set; }
        public int BlockSheddingWeight { get; private set; }
        public int BreakSackWeight { get; private set; }
        public int CarryingWeight { get; private set; }
        public int CatchingWeight { get; private set; }
        public int ChangeOfDirectionWeight { get; private set; }
        public int DeepRouteRunningWeight { get; private set; }
        public int DeepThrowAccuracyWeight { get; private set; }
        public int FinesseMovesWeight { get; private set; }
        public int HitPowerWeight { get; private set; }
        public int ImpactBlockingWeight { get; private set; }
        public int InjuryWeight { get; private set; }
        public int JukeMoveWeight { get; private set; }
        public int JumpingWeight { get; private set; }
        public int KickAccuracyWeight { get; private set; }
        public int KickReturnWeight { get; private set; }
        public int LeadBlockWeight { get; private set; }
        public int ManCoverageWeight { get; private set; }
        public int MediumRouteRunningWeight { get; private set; }
        public int MediumThrowAccuracyWeight { get; private set; }
        public int PassBlockWeight { get; private set; }
        public int PassBlockFinesseWeight { get; private set; }
        public int PassBlockPowerWeight { get; private set; }
        public int PlayActionWeight { get; private set; }
        public int PlayRecognitionWeight { get; private set; }
        public int PowerMovesWeight { get; private set; }
        public int PressWeight { get; private set; }
        public int PursuitWeight { get; private set; }
        public int ReleaseWeight { get; private set; }
        public int RunBlockWeight { get; private set; }
        public int RunBlockFinesseWeight { get; private set; }
        public int RunBlockPowerWeight { get; private set; }
        public int ShortRouteRunningWeight { get; private set; }
        public int ShortThrowAccuracyWeight { get; private set; }
        public int SpectacularCatchWeight { get; private set; }
        public int SpinMoveWeight { get; private set; }
        public int StaminaWeight { get; private set; }
        public int StiffArmWeight { get; private set; }
        public int ThrowUnderPressureWeight { get; private set; }
        public int ThrowingOnTheRunWeight { get; private set; }
        public int ToughnessWeight { get; private set; }
        public int TruckingWeight { get; private set; }
        public int ZoneCoverageWeight { get; private set; }
        public int HeightWeight { get; private set; }
        public int WeightWeight { get; private set; }
        public int MaxHeight { get; private set; }
        public int MaxWeight { get; private set; }

        // File path to store the stat values
        private readonly string defaultStatsFilePath = "weights.json";

        public PlayerStatsEntryWindow()
        {
            InitializeComponent();
            LoadStats();
        }


        private void LoadStats()
        {
            try
            {
                string statsFilePath = ConfigurationNameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(statsFilePath))
                {
                    statsFilePath = defaultStatsFilePath;
                }

                if (File.Exists(statsFilePath))
                {
                    // Read the stat values from the JSON file
                    string json = File.ReadAllText(statsFilePath);
                    var stats = JsonConvert.DeserializeObject<PlayerStats>(json);
                    if (stats != null)
                    {
                        SpeedTextBox.Text = stats.SpeedWeight.ToString();
                        AccelerationTextBox.Text = stats.AccelerationWeight.ToString();
                        AgilityTextBox.Text = stats.AgilityWeight.ToString();
                        StrengthTextBox.Text = stats.StrengthWeight.ToString();
                        AwarenessTextBox.Text = stats.AwarenessWeight.ToString();
                        ThrowPowerTextBox.Text = stats.ThrowPowerWeight.ToString();
                        ThrowAccuracyTextBox.Text = stats.ThrowAccuracyWeight.ToString();
                        CatchInTrafficTextBox.Text = stats.CatchInTrafficWeight.ToString();
                        TackleTextBox.Text = stats.TackleWeight.ToString();
                        BreakTackleTextBox.Text = stats.BreakTackleWeight.ToString();
                        KickPowerTextBox.Text = stats.KickPowerWeight.ToString();
                        AccelerationTextBox.Text = stats.AccelerationWeight.ToString();
                        BallCarrierVisionTextBox.Text = stats.BallCarrierVisionWeight.ToString();
                        BlockSheddingTextBox.Text = stats.BlockSheddingWeight.ToString();
                        BreakSackTextBox.Text = stats.BreakSackWeight.ToString();
                        CarryingTextBox.Text = stats.CarryingWeight.ToString();
                        CatchingTextBox.Text = stats.CatchingWeight.ToString();
                        ChangeOfDirectionTextBox.Text = stats.ChangeOfDirectionWeight.ToString();
                        DeepRouteRunningTextBox.Text = stats.DeepRouteRunningWeight.ToString();
                        DeepThrowAccuracyTextBox.Text = stats.DeepThrowAccuracyWeight.ToString();
                        FinesseMovesTextBox.Text = stats.FinesseMovesWeight.ToString();
                        HitPowerTextBox.Text = stats.HitPowerWeight.ToString();
                        ImpactBlockingTextBox.Text = stats.ImpactBlockingWeight.ToString();
                        InjuryTextBox.Text = stats.InjuryWeight.ToString();
                        JukeMoveTextBox.Text = stats.JukeMoveWeight.ToString();
                        JumpingTextBox.Text = stats.JumpingWeight.ToString();
                        KickAccuracyTextBox.Text = stats.KickAccuracyWeight.ToString();
                        KickReturnTextBox.Text = stats.KickReturnWeight.ToString();
                        LeadBlockTextBox.Text = stats.LeadBlockWeight.ToString();
                        ManCoverageTextBox.Text = stats.ManCoverageWeight.ToString();
                        MediumRouteRunningTextBox.Text = stats.MediumRouteRunningWeight.ToString();
                        MediumThrowAccuracyTextBox.Text = stats.MediumThrowAccuracyWeight.ToString();
                        PassBlockingTextBox.Text = stats.PassBlockWeight.ToString();
                        PassBlockFinesseTextBox.Text = stats.PassBlockFinesseWeight.ToString();
                        PassBlockStrengthTextBox.Text = stats.PassBlockPowerWeight.ToString();
                        PlayActionTextBox.Text = stats.PlayActionWeight.ToString();
                        PlayRecognitionTextBox.Text = stats.PlayRecognitionWeight.ToString();
                        PowerMovesTextBox.Text = stats.PowerMovesWeight.ToString();
                        PressTextBox.Text = stats.PressWeight.ToString();
                        PursuitTextBox.Text = stats.PursuitWeight.ToString();
                        ReleaseTextBox.Text = stats.ReleaseWeight.ToString();
                        RunBlockTextBox.Text = stats.RunBlockWeight.ToString();
                        RunBlockFinesseTextBox.Text = stats.RunBlockFinesseWeight.ToString();
                        RunBlockStrengthTextBox.Text = stats.RunBlockPowerWeight.ToString();
                        ShortRouteRunningTextBox.Text = stats.ShortRouteRunningWeight.ToString();
                        ShortThrowAccuracyTextBox.Text = stats.ShortThrowAccuracyWeight.ToString();
                        SpectacularCatchTextBox.Text = stats.SpectacularCatchWeight.ToString();
                        SpinMoveTextBox.Text = stats.SpinMoveWeight.ToString();
                        StaminaTextBox.Text = stats.StaminaWeight.ToString();
                        StiffArmTextBox.Text = stats.StiffArmWeight.ToString();
                        ThrowUnderPressureTextBox.Text = stats.ThrowUnderPressureWeight.ToString();
                        ThrowingOnTheRunTextBox.Text = stats.ThrowingOnTheRunWeight.ToString();
                        TruckingTextBox.Text = stats.TruckingWeight.ToString();
                        ZoneCoverageTextBox.Text = stats.ZoneCoverageWeight.ToString();
                        HeightTextBox.Text = stats.HeightWeight.ToString();
                        WeightTextBox.Text = stats.WeightWeight.ToString();
                        MaxHeightTextBox.Text = stats.MaxHeight.ToString();
                        MaxWeightTextBox.Text = stats.MaxWeight.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stats: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveStats()
        {
            try
            {
                string statsFilePath = ConfigurationNameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(statsFilePath))
                {
                    statsFilePath = defaultStatsFilePath;
                }

                // Ensure that the file extension is ".json"
                if (!statsFilePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    statsFilePath += ".json";
                }

                // Serialize the stat values to JSON
                var stats = new PlayerStats
                {
                    AccelerationWeight = int.Parse(AccelerationTextBox.Text),
                    SpeedWeight = int.Parse(SpeedTextBox.Text),
                    AgilityWeight = int.Parse(AgilityTextBox.Text),
                    StrengthWeight = int.Parse(StrengthTextBox.Text),
                    AwarenessWeight = int.Parse(AwarenessTextBox.Text),
                    ThrowPowerWeight = int.Parse(ThrowPowerTextBox.Text),
                    ThrowAccuracyWeight = int.Parse(ThrowAccuracyTextBox.Text),
                    CatchInTrafficWeight = int.Parse(CatchInTrafficTextBox.Text),
                    TackleWeight = int.Parse(TackleTextBox.Text),
                    BreakTackleWeight = int.Parse(BreakTackleTextBox.Text),
                    KickPowerWeight = int.Parse(KickPowerTextBox.Text),
                    BallCarrierVisionWeight = int.Parse(BallCarrierVisionTextBox.Text),
                    BlockSheddingWeight = int.Parse(BlockSheddingTextBox.Text),
                    BreakSackWeight = int.Parse(BreakSackTextBox.Text),
                    CarryingWeight = int.Parse(CarryingTextBox.Text),
                    CatchingWeight = int.Parse(CatchingTextBox.Text),
                    ChangeOfDirectionWeight = int.Parse(ChangeOfDirectionTextBox.Text),
                    DeepRouteRunningWeight = int.Parse(DeepRouteRunningTextBox.Text),
                    DeepThrowAccuracyWeight = int.Parse(DeepThrowAccuracyTextBox.Text),
                    FinesseMovesWeight = int.Parse(FinesseMovesTextBox.Text),
                    HitPowerWeight = int.Parse(HitPowerTextBox.Text),
                    ImpactBlockingWeight = int.Parse(ImpactBlockingTextBox.Text),
                    InjuryWeight = int.Parse(InjuryTextBox.Text),
                    JukeMoveWeight = int.Parse(JukeMoveTextBox.Text),
                    JumpingWeight = int.Parse(JumpingTextBox.Text),
                    KickAccuracyWeight = int.Parse(KickAccuracyTextBox.Text),
                    KickReturnWeight = int.Parse(KickReturnTextBox.Text),
                    LeadBlockWeight = int.Parse(LeadBlockTextBox.Text),
                    ManCoverageWeight = int.Parse(ManCoverageTextBox.Text),
                    MediumRouteRunningWeight = int.Parse(MediumRouteRunningTextBox.Text),
                    MediumThrowAccuracyWeight = int.Parse(MediumThrowAccuracyTextBox.Text),
                    PassBlockWeight = int.Parse(PassBlockingTextBox.Text),
                    PassBlockFinesseWeight = int.Parse(PassBlockFinesseTextBox.Text),
                    PassBlockPowerWeight = int.Parse(PassBlockStrengthTextBox.Text),
                    PlayActionWeight = int.Parse(PlayActionTextBox.Text),
                    PlayRecognitionWeight = int.Parse(PlayRecognitionTextBox.Text),
                    PowerMovesWeight = int.Parse(PowerMovesTextBox.Text),
                    PressWeight = int.Parse(PressTextBox.Text),
                    PursuitWeight = int.Parse(PursuitTextBox.Text),
                    ReleaseWeight = int.Parse(ReleaseTextBox.Text),
                    RunBlockWeight = int.Parse(RunBlockTextBox.Text),
                    RunBlockFinesseWeight = int.Parse(RunBlockFinesseTextBox.Text),
                    RunBlockPowerWeight = int.Parse(RunBlockStrengthTextBox.Text),
                    ShortRouteRunningWeight = int.Parse(ShortRouteRunningTextBox.Text),
                    ShortThrowAccuracyWeight = int.Parse(ShortThrowAccuracyTextBox.Text),
                    SpectacularCatchWeight = int.Parse(SpectacularCatchTextBox.Text),
                    SpinMoveWeight = int.Parse(SpinMoveTextBox.Text),
                    StaminaWeight = int.Parse(StaminaTextBox.Text),
                    StiffArmWeight = int.Parse(StiffArmTextBox.Text),
                    ThrowUnderPressureWeight = int.Parse(ThrowUnderPressureTextBox.Text),
                    ThrowingOnTheRunWeight = int.Parse(ThrowingOnTheRunTextBox.Text),
                    TruckingWeight = int.Parse(TruckingTextBox.Text),
                    ZoneCoverageWeight = int.Parse(ZoneCoverageTextBox.Text),
                    HeightWeight = int.Parse(HeightTextBox.Text),
                    WeightWeight = int.Parse(WeightTextBox.Text),
                    MaxWeight = int.Parse(MaxWeightTextBox.Text),
                    MaxHeight = int.Parse(MaxHeightTextBox.Text)
                };
                string json = JsonConvert.SerializeObject(stats);

                // Write the JSON to the file
                File.WriteAllText(statsFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving stats: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the stat values
            SaveStats();

            DialogResult = true; // Close the window with DialogResult set to true
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Close the window with DialogResult set to false
        }
    }

    // Class to store player stat values
    public class PlayerStats
    {
        public int SpeedWeight { get; set; }
        public int AgilityWeight { get; set; }
        public int StrengthWeight { get; set; }
        public int AwarenessWeight { get; set; }
        public int ThrowPowerWeight { get; set; }
        public int ThrowAccuracyWeight { get; set; }
        public int CatchInTrafficWeight { get; set; }
        public int TackleWeight { get; set; }
        public int BreakTackleWeight { get; set; }
        public int KickPowerWeight { get; set; }
        public int AccelerationWeight { get; set; }
        public int BallCarrierVisionWeight { get; set; }
        public int BlockSheddingWeight { get; set; }
        public int BreakSackWeight { get; set; }
        public int CarryingWeight { get; set; }
        public int CatchingWeight { get; set; }
        public int ChangeOfDirectionWeight { get; set; }
        public int DeepRouteRunningWeight { get; set; }
        public int DeepThrowAccuracyWeight { get; set; }
        public int FinesseMovesWeight { get; set; }
        public int HitPowerWeight { get; set; }
        public int ImpactBlockingWeight { get; set; }
        public int InjuryWeight { get; set; }
        public int JukeMoveWeight { get; set; }
        public int JumpingWeight { get; set; }
        public int KickAccuracyWeight { get; set; }
        public int KickReturnWeight { get; set; }
        public int LeadBlockWeight { get; set; }
        public int ManCoverageWeight { get; set; }
        public int MediumRouteRunningWeight { get; set; }
        public int MediumThrowAccuracyWeight { get; set; }
        public int PassBlockWeight { get; set; }
        public int PassBlockFinesseWeight { get; set; }
        public int PassBlockPowerWeight { get; set; }
        public int PlayActionWeight { get; set; }
        public int PlayRecognitionWeight { get; set; }
        public int PowerMovesWeight { get; set; }
        public int PressWeight { get; set; }
        public int PursuitWeight { get; set; }
        public int ReleaseWeight { get; set; }
        public int RunBlockWeight { get; set; }
        public int RunBlockFinesseWeight { get; set; }
        public int RunBlockPowerWeight { get; set; }
        public int ShortRouteRunningWeight { get; set; }
        public int ShortThrowAccuracyWeight { get; set; }
        public int SpectacularCatchWeight { get; set; }
        public int SpinMoveWeight { get; set; }
        public int StaminaWeight { get; set; }
        public int StiffArmWeight { get; set; }
        public int ThrowUnderPressureWeight { get; set; }
        public int ThrowingOnTheRunWeight { get; set; }
        public int ToughnessWeight { get; set; }
        public int TruckingWeight { get; set; }
        public int ZoneCoverageWeight { get; set; }
        public int HeightWeight { get; set; }
        public int WeightWeight { get; set; }
        public int MaxWeight { get; set; }
        public int MaxHeight { get; set; }
    }
}