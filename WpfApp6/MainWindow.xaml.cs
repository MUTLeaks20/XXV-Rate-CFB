using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static JsonDataDisplay.MainWindow;

namespace JsonDataDisplay
{
    public partial class MainWindow : Window
    {
        private List<Datum> allPlayers;
        private Datum selectedPlayerLeftClick;

        public MainWindow()
        {
            InitializeComponent();
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            try
            {
                string directory = "playerdata";
                string[] files = Directory.GetFiles(directory, "*.json");

                allPlayers = new List<Datum>();

                // Define lists for other filters
                List<string> positionList = new List<string>();
                List<string> programList = new List<string>();
                List<string> archetypeList = new List<string>();

                // Clear ComboBox items before populating them
                TeamFilterComboBox.Items.Clear();
                PositionFilterComboBox.Items.Clear();
                ProgramFilterComboBox.Items.Clear();
                ArchetypeFilterComboBox.Items.Clear();

                foreach (var file in files)
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string json = reader.ReadToEnd();
                        var data = JsonConvert.DeserializeObject<MyData>(json);
                        allPlayers.AddRange(data.data);

                        // Populate teamList, positionList, programList, and archetypeList
                        foreach (var player in data.data)
                        {
                            if (player.availableChemistry != null)
                            {
                                foreach (var chemistry in player.availableChemistry)
                                {
                                    // Populate team filter
                                    if (!string.IsNullOrEmpty(chemistry.name) && !TeamFilterComboBox.Items.Contains(chemistry.name))
                                    {
                                        TeamFilterComboBox.Items.Add(chemistry.name);
                                    }
                                }
                            }

                            // Populate other filters
                            if (!string.IsNullOrEmpty(player.position?.name) && !positionList.Contains(player.position.name))
                            {
                                positionList.Add(player.position.name);
                            }

                            if (!string.IsNullOrEmpty(player.program?.name) && !programList.Contains(player.program.name))
                            {
                                programList.Add(player.program.name);
                            }

                            if (!string.IsNullOrEmpty(player.archetype?.name) && !archetypeList.Contains(player.archetype.name))
                            {
                                archetypeList.Add(player.archetype.name);
                            }
                        }
                    }
                }

                // Populate other filters
                PositionFilterComboBox.Items.Add("All Positions"); // Add "All Positions"
                foreach (var position in positionList)
                {
                    PositionFilterComboBox.Items.Add(position);
                }

                ProgramFilterComboBox.Items.Add("All Programs"); // Add "All Programs"
                foreach (var program in programList)
                {
                    ProgramFilterComboBox.Items.Add(program);
                }

                ArchetypeFilterComboBox.Items.Add("All Archetypes"); // Add "All Archetypes"
                foreach (var archetype in archetypeList)
                {
                    ArchetypeFilterComboBox.Items.Add(archetype);
                }

                // Add "All Teams" to the ComboBox
                TeamFilterComboBox.Items.Insert(0, "All Teams");

                // Select first item in each ComboBox
                TeamFilterComboBox.SelectedIndex = 0;
                PositionFilterComboBox.SelectedIndex = 0;
                ProgramFilterComboBox.SelectedIndex = 0;
                ArchetypeFilterComboBox.SelectedIndex = 0;

                // Check if allPlayers is not null before filtering
                if (allPlayers != null)
                {
                    FilterPlayers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private bool isRatingCalculationInProgress = false;
        private void NameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterPlayers();
        }

        private void FilterPlayers()
        {
            if (allPlayers == null || TeamFilterComboBox.Items.Count == 0 || PositionFilterComboBox.Items.Count == 0 || ProgramFilterComboBox.Items.Count == 0 || ArchetypeFilterComboBox.Items.Count == 0)
                return;

            try
            {
                // Retrieve and validate overall range
                if (!int.TryParse(OverallLowerBoundTextBox.Text, out int overallLower) || !int.TryParse(OverallHigherBoundTextBox.Text, out int overallHigher))
                {
                    MessageBox.Show("Please enter valid overall range values.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Ensure the range is logical
                if (overallLower > overallHigher)
                {
                    MessageBox.Show("Overall lower bound should be less than or equal to overall higher bound.", "Invalid Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var filteredPlayers = allPlayers
                    .OrderByDescending(p => p.overall)
                    .Where(p => (TeamFilterComboBox.SelectedIndex == 0 || (p.availableChemistry != null && p.availableChemistry.Any(c => c.name == (string)TeamFilterComboBox.SelectedItem)))
                                && (PositionFilterComboBox.SelectedIndex == 0 || p.position?.name == (string)PositionFilterComboBox.SelectedItem)
                                && (ProgramFilterComboBox.SelectedIndex == 0 || p.program?.name == (string)ProgramFilterComboBox.SelectedItem)
                                && (ArchetypeFilterComboBox.SelectedIndex == 0 || p.archetype?.name == (string)ArchetypeFilterComboBox.SelectedItem)
                                && (string.IsNullOrEmpty(FullNameTextBox.Text.Trim().ToLower()) || (p.firstName + " " + p.lastName).ToLower().Contains(FullNameTextBox.Text.Trim().ToLower()))
                                && (p.overall >= overallLower && p.overall <= overallHigher)) // Filter based on original overall
                    .OrderByDescending(p => p.newOverall)
                    .Take(300)
                    .ToList();

                // Display filtered players
                PlayersList.ItemsSource = filteredPlayers;

                // Load card images for filtered players
                LoadCardImages(filteredPlayers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering players: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateFilteredPlayers(List<Datum> players)
        {
            // Recalculate new overall ratings for filtered players
            CalculateNewOverall(players);

            // Sort by new overall rating if calculation was applied

            players = players.OrderByDescending(p => p.newOverall).ToList();




            LoadCardImages(players);
        }

        public class MyData
        {
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public int pk { get; set; }
            public int externalId { get; set; }
            public string gameSlug { get; set; }
            public string url { get; set; }
            public int overall { get; set; }
            public int maxOverall { get; set; }
            public CardImage cardImage { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public Team team { get; set; }
            public Position position { get; set; } // Added position property
            public Program program { get; set; }
            public Archetype archetype { get; set; } // Added archetype property
            public bool canAuction { get; set; }
            public bool canTrade { get; set; }
            public bool isMaxedOvr { get; set; }
            public bool hasPowerUp { get; set; }
            public bool isLtd { get; set; }
            public bool MaxWeight { get; set; }
            public int MaxHeight { get; set; }

            public List<AvailableChemistry> availableChemistry { get; set; }

            // New attributes
            public int heightInches { get; set; } // Assuming height is in inches
            public int weightPounds { get; set; } // Assuming weight is in pounds

            // Normalized attributes
            public double normalizedHeight { get; set; }
            public double normalizedWeight { get; set; }

            public int acceleration { get; set; }
            public int agility { get; set; }
            public int awareness { get; set; }
            public int ballCarrierVision { get; set; }
            public int blockShedding { get; set; }
            public int breakSack { get; set; }
            public int breakTackle { get; set; }
            public int carrying { get; set; }
            public int catchInTraffic { get; set; }
            public int catching { get; set; }
            public int changeOfDirection { get; set; }
            public int deepRouteRunning { get; set; }
            public int deepThrowAccuracy { get; set; }
            public int finesseMoves { get; set; }
            public int hitPower { get; set; }
            public int impactBlocking { get; set; }
            public int injury { get; set; }
            public int jukeMove { get; set; }
            public int jumping { get; set; }
            public int kickAccuracy { get; set; }
            public int kickPower { get; set; }
            public int kickReturn { get; set; }
            public int leadBlock { get; set; }
            public int manCoverage { get; set; }
            public int mediumRouteRunning { get; set; }
            public int mediumThrowAccuracy { get; set; }
            public int passBlock { get; set; }
            public int passBlockFinesse { get; set; }
            public int passBlockPower { get; set; }
            public int playAction { get; set; }
            public int playRecognition { get; set; }
            public int powerMoves { get; set; }
            public int press { get; set; }
            public int pursuit { get; set; }
            public int release { get; set; }
            public int runBlock { get; set; }
            public int runBlockFinesse { get; set; }
            public int runBlockPower { get; set; }
            public int shortRouteRunning { get; set; }
            public int shortThrowAccuracy { get; set; }
            public int spectacularCatch { get; set; }
            public int speed { get; set; }
            public int spinMove { get; set; }
            public int stamina { get; set; }
            public int stiffArm { get; set; }
            public int strength { get; set; }
            public int tackle { get; set; }
            public int throwAccuracy { get; set; }
            public int throwPower { get; set; }
            public int throwUnderPressure { get; set; }
            public int throwingOnTheRun { get; set; }
            public int toughness { get; set; }
            public int trucking { get; set; }
            public int zoneCoverage { get; set; }

            public int newOverall { get; set; }

            public class WeightData
            {
                public int AccelerationStat { get; set; }
                public int AgilityStat { get; set; }
                public int AwarenessStat { get; set; }
                public int BallCarrierVisionStat { get; set; }
                public int BlockSheddingStat { get; set; }
                public int BreakSackStat { get; set; }
                public int BreakTackleStat { get; set; }
                public int CarryingStat { get; set; }
                public int CatchInTrafficStat { get; set; }
                public int CatchingStat { get; set; }
                public int ChangeOfDirectionStat { get; set; }
                public int DeepRouteRunningStat { get; set; }
                public int DeepThrowAccuracyStat { get; set; }
                public int FinesseMovesStat { get; set; }
                public int HitPowerStat { get; set; }
                public int ImpactBlockingStat { get; set; }
                public int InjuryStat { get; set; }
                public int JukeMoveStat { get; set; }
                public int JumpingStat { get; set; }
                public int KickAccuracyStat { get; set; }
                public int KickPowerStat { get; set; }
                public int KickReturnStat { get; set; }
                public int LeadBlockStat { get; set; }
                public int ManCoverageStat { get; set; }
                public int MediumRouteRunningStat { get; set; }
                public int MediumThrowAccuracyStat { get; set; }
                public int PassBlockStat { get; set; }
                public int PassBlockFinesseStat { get; set; }
                public int PassBlockPowerStat { get; set; }
                public int PlayActionStat { get; set; }
                public int PlayRecognitionStat { get; set; }
                public int PowerMovesStat { get; set; }
                public int PressStat { get; set; }
                public int PursuitStat { get; set; }
                public int ReleaseStat { get; set; }
                public int RunBlockStat { get; set; }
                public int RunBlockFinesseStat { get; set; }
                public int RunBlockPowerStat { get; set; }
                public int ShortRouteRunningStat { get; set; }
                public int ShortThrowAccuracyStat { get; set; }
                public int SpectacularCatchStat { get; set; }
                public int SpeedStat { get; set; }
                public int SpinMoveStat { get; set; }
                public int StaminaStat { get; set; }
                public int StiffArmStat { get; set; }
                public int StrengthStat { get; set; }
                public int TackleStat { get; set; }
                public int ThrowAccuracyStat { get; set; }
                public int ThrowPowerStat { get; set; }
                public int ThrowUnderPressureStat { get; set; }
                public int ThrowingOnTheRunStat { get; set; }
                public int ToughnessStat { get; set; }
                public int TruckingStat { get; set; }
                public int ZoneCoverageStat { get; set; }
            }


            public void NormalizeAttributes(int maxHeight, int maxWeight)
            {

                // Normalize height
                normalizedHeight = (double) heightInches / maxHeight;

                // Normalize weight
                normalizedWeight = (double) weightPounds / maxWeight;
            }




            public void CalculateNewOverall(
                int speedWeight, int accelerationWeight, int agilityWeight, int awarenessWeight, int ballCarrierVisionWeight, int blockSheddingWeight,
                int breakSackWeight, int breakTackleWeight, int carryingWeight, int catchInTrafficWeight, int catchingWeight,
                int changeOfDirectionWeight, int deepRouteRunningWeight, int deepThrowAccuracyWeight, int finesseMovesWeight,
                int hitPowerWeight, int impactBlockingWeight, int injuryWeight, int jukeMoveWeight, int jumpingWeight,
                int kickAccuracyWeight, int kickPowerWeight, int kickReturnWeight, int leadBlockWeight, int manCoverageWeight,
                int mediumRouteRunningWeight, int mediumThrowAccuracyWeight, int passBlockWeight, int passBlockFinesseWeight,
                int passBlockPowerWeight, int playActionWeight, int playRecognitionWeight, int powerMovesWeight, int pressWeight,
                int pursuitWeight, int releaseWeight, int runBlockWeight, int runBlockFinesseWeight, int runBlockPowerWeight,
                int shortRouteRunningWeight, int shortThrowAccuracyWeight, int spectacularCatchWeight,
                int spinMoveWeight, int staminaWeight, int stiffArmWeight, int strengthWeight, int tackleWeight,
                int throwAccuracyWeight, int throwPowerWeight, int throwUnderPressureWeight, int throwingOnTheRunWeight,
                int toughnessWeight, int truckingWeight, int zoneCoverageWeight, int heightWeight, int weightWeight, int maxWeight, int maxHeight)
            {
                newOverall = (
                    speedWeight * this.speed +
                    agilityWeight * this.agility +
                    accelerationWeight * this.acceleration +
                    awarenessWeight * this.awareness +
                    ballCarrierVisionWeight * this.ballCarrierVision +
                    blockSheddingWeight * this.blockShedding +
                    breakSackWeight * this.breakSack +
                    breakTackleWeight * this.breakTackle +
                    carryingWeight * this.carrying +
                    catchInTrafficWeight * this.catchInTraffic +
                    catchingWeight * this.catching +
                    changeOfDirectionWeight * this.changeOfDirection +
                    deepRouteRunningWeight * this.deepRouteRunning +
                    deepThrowAccuracyWeight * this.deepThrowAccuracy +
                    finesseMovesWeight * this.finesseMoves +
                    hitPowerWeight * this.hitPower +
                    impactBlockingWeight * this.impactBlocking +
                    injuryWeight * this.injury +
                    jukeMoveWeight * this.jukeMove +
                    jumpingWeight * this.jumping +
                    kickAccuracyWeight * this.kickAccuracy +
                    kickPowerWeight * this.kickPower +
                    kickReturnWeight * this.kickReturn +
                    leadBlockWeight * this.leadBlock +
                    manCoverageWeight * this.manCoverage +
                    mediumRouteRunningWeight * this.mediumRouteRunning +
                    mediumThrowAccuracyWeight * this.mediumThrowAccuracy +
                    passBlockWeight * this.passBlock +
                    passBlockFinesseWeight * this.passBlockFinesse +
                    passBlockPowerWeight * this.passBlockPower +
                    playActionWeight * this.playAction +
                    playRecognitionWeight * this.playRecognition +
                    powerMovesWeight * this.powerMoves +
                    pressWeight * this.press +
                    pursuitWeight * this.pursuit +
                    releaseWeight * this.release +
                    runBlockWeight * this.runBlock +
                    runBlockFinesseWeight * this.runBlockFinesse +
                    runBlockPowerWeight * this.runBlockPower +
                    shortRouteRunningWeight * this.shortRouteRunning +
                    shortThrowAccuracyWeight * this.shortThrowAccuracy +
                    spectacularCatchWeight * this.spectacularCatch +
                    spinMoveWeight * this.spinMove +
                    staminaWeight * this.stamina +
                    stiffArmWeight * this.stiffArm +
                    strengthWeight * this.strength +
                    tackleWeight * this.tackle +
                    throwAccuracyWeight * this.throwAccuracy +
                    throwPowerWeight * this.throwPower +
                    throwUnderPressureWeight * this.throwUnderPressure +
                    throwingOnTheRunWeight * this.throwingOnTheRun +
                    toughnessWeight * this.toughness +
                    truckingWeight * this.trucking +
                    heightWeight * (int)(normalizedHeight * 100) +
                    weightWeight * (int)(normalizedWeight * 100) +
                    zoneCoverageWeight * this.zoneCoverage
                ) / 100;
            }
        }

        public class CardImage
        {
            public string url { get; set; }
        }

        public class Team
        {
            public string name { get; set; }
            public int id { get; set; }
            public string abbreviation { get; set; }
            public string slug { get; set; }
        }

        public class Position
        {
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class Program
        {
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class Archetype
        {
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class AvailableChemistry
        {
            public string name { get; set; }
            public int id { get; set; }
            public string slug { get; set; }
            public string abbreviation { get; set; }
        }


        private void TeamFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPlayers();
        }

        private void PositionFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPlayers();
        }

        private void ProgramFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPlayers();
        }

        private void ArchetypeFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPlayers();
        }

        private void PlayersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var player = (Datum)PlayersList.SelectedItem;
            if (player != null)
            {
                string url = "https://cfb.fan" + player.url;
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening URL: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlayerLeftClick = (Datum)PlayersList.SelectedItem;
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero; // Reset the position to zero to loop the video
        }

        private void FullNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FilterPlayers();
            }
        }
        private void EnterStatsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the pop-up window
            PlayerStatsEntryWindow statsEntryWindow = new PlayerStatsEntryWindow();
            statsEntryWindow.ShowDialog(); // Show the window as a modal dialog
        }

        private void PlayerCard_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListBoxItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep != null && dep is ListBoxItem item)
                {
                    selectedPlayerLeftClick = (Datum)item.DataContext;
                    if (!item.IsSelected)
                    {
                        item.IsSelected = true; // Manually select the item
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling left-click event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PlayerCard_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListBoxItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep != null && dep is ListBoxItem item)
                {
                    Datum rightClickedPlayer = (Datum)item.DataContext;

                    Datum leftClickedPlayer = selectedPlayerLeftClick;

                    if (leftClickedPlayer != null && rightClickedPlayer != null)
                    {
                        string url = $"https://cfb.fan/compare/#{leftClickedPlayer.externalId}:0,{rightClickedPlayer.externalId}:0";
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error opening URL: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a player by left-clicking on a card first!");
                    }
                }
                else
                {
                    MessageBox.Show("No player selected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling right-click event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CalculateNewOverall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateNewOverall(allPlayers);
                FilterPlayers();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating new overall: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateNewOverall(List<Datum> players)
    {
        try
        {
            // Read weights from JSON file
            string jsonFilePath = "weights.json"; // Update with your JSON file path
            if (!File.Exists(jsonFilePath))
            {
                MessageBox.Show("Weights JSON file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string json = File.ReadAllText(jsonFilePath);
            var weights = JsonConvert.DeserializeObject<WeightSettings>(json);

            CalculateNewOverall(players, weights);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error calculating new overall: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CalculateNewOverall(List<Datum> players, WeightSettings weights)
    {
        try
        {
            if (weights == null)
            {
                MessageBox.Show("Error loading weights from JSON.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var player in players)
            {
                    int maxHeight = weights.MaxHeight;
                    int maxWeight = weights.MaxWeight;
                    player.NormalizeAttributes(maxHeight, maxWeight);


                    player.CalculateNewOverall(
                        weights.SpeedWeight, weights.AccelerationWeight, weights.AgilityWeight, weights.AwarenessWeight, weights.BallCarrierVisionWeight,
                        weights.BlockSheddingWeight, weights.BreakSackWeight, weights.BreakTackleWeight, weights.CarryingWeight,
                        weights.CatchInTrafficWeight, weights.CatchingWeight, weights.ChangeOfDirectionWeight, weights.DeepRouteRunningWeight,
                        weights.DeepThrowAccuracyWeight, weights.FinesseMovesWeight, weights.HitPowerWeight, weights.ImpactBlockingWeight,
                        weights.InjuryWeight, weights.JukeMoveWeight, weights.JumpingWeight, weights.KickAccuracyWeight, weights.KickPowerWeight,
                        weights.KickReturnWeight, weights.LeadBlockWeight, weights.ManCoverageWeight, weights.MediumRouteRunningWeight,
                        weights.MediumThrowAccuracyWeight, weights.PassBlockWeight, weights.PassBlockFinesseWeight, weights.PassBlockPowerWeight,
                        weights.PlayActionWeight, weights.PlayRecognitionWeight, weights.PowerMovesWeight, weights.PressWeight, weights.PursuitWeight,
                        weights.ReleaseWeight, weights.RunBlockWeight, weights.RunBlockFinesseWeight, weights.RunBlockPowerWeight,
                        weights.ShortRouteRunningWeight, weights.ShortThrowAccuracyWeight, weights.SpectacularCatchWeight, weights.SpinMoveWeight,
                        weights.StaminaWeight, weights.StiffArmWeight, weights.StrengthWeight, weights.TackleWeight, weights.ThrowAccuracyWeight,
                        weights.ThrowPowerWeight, weights.ThrowUnderPressureWeight, weights.ThrowingOnTheRunWeight, weights.ToughnessWeight,
                        weights.TruckingWeight, weights.ZoneCoverageWeight, weights.HeightWeight, weights.WeightWeight, weights.MaxWeight, weights.MaxHeight);
                }

            // Sort the filtered players by the new calculated overall value
            players = players.OrderByDescending(p => p.newOverall).ToList();

            PlayersList.ItemsSource = players;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error calculating new overall: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Define a class to hold weight settings
    public class WeightSettings
    {
            public int AccelerationWeight { get; set; }
            public int HeightWeight { get; set; }
            public int WeightWeight { get; set; }
            public int MaxWeight { get; set; }
            public int MaxHeight { get; set; }
            public int AgilityWeight { get; set; }
            public int AwarenessWeight { get; set; }
            public int BallCarrierVisionWeight { get; set; }
            public int BlockSheddingWeight { get; set; }
            public int BreakSackWeight { get; set; }
            public int BreakTackleWeight { get; set; }
            public int CarryingWeight { get; set; }
            public int CatchInTrafficWeight { get; set; }
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
            public int KickPowerWeight { get; set; }
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
            public int SpeedWeight { get; set; }
            public int SpinMoveWeight { get; set; }
            public int StaminaWeight { get; set; }
            public int StiffArmWeight { get; set; }
            public int StrengthWeight { get; set; }
            public int TackleWeight { get; set; }
            public int ThrowAccuracyWeight { get; set; }
            public int ThrowPowerWeight { get; set; }
            public int ThrowUnderPressureWeight { get; set; }
            public int ThrowingOnTheRunWeight { get; set; }
            public int ToughnessWeight { get; set; }
            public int TruckingWeight { get; set; }
            public int ZoneCoverageWeight { get; set; }
        }

    private void LoadCardImages(List<Datum> players)
        {
            try
            {
                foreach (var player in players)
                {
                    string imageName = $"{player.externalId}.png";
                    string imagePath = $"imagedata/{imageName}";
                    string imageFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

                    if (File.Exists(imageFullPath))
                    {
                        player.cardImage = new CardImage { url = new Uri(imageFullPath).AbsoluteUri };
                    }
                    else
                    {
                        player.cardImage = new CardImage { url = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagedata/default.png")).AbsoluteUri };
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the image directory is unauthorized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("The path to the image directory is too long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OverallHigherBoundTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OverallHigherBoundTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void PlayersList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}