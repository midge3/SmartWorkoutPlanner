/*
 * Collin Wanta
 * Smart Workout Planner
 * Controller
 */
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SmartWorkout_Planner
{
    public partial class MainWindow : Window
    {
        private readonly WorkoutPlanModel _workoutPlanModel;

        public MainWindow()
        {
            InitializeComponent();
            _workoutPlanModel = new WorkoutPlanModel();

            // Event handlers
            GeneratePlanButton.Click += GeneratePlanButton_Click;
            SavePlanButton.Click += SavePlanButton_Click;
            LoadPlanButton.Click += LoadPlanButton_Click;
        }

        private async void GeneratePlanButton_Click(object sender, RoutedEventArgs e)
        {
            // Get user inputs
            var workoutType = (WorkoutTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var daysPerWeekString = (DaysPerWeekComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(workoutType) || string.IsNullOrEmpty(daysPerWeekString))// check if the user has selected a workout type and days per week
            {
                MessageBox.Show("Please select both a workout type and the number of days per week.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(daysPerWeekString, out int daysPerWeek))// check if the days per week is a number that is valid
            {
                MessageBox.Show("Invalid number of days per week.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Generate a workout plan using the WorkoutPlanModel on a background thread(multithreading)
            try
            {
                WorkoutPlanTextBlock.Text = "Generating workout plan...";
                var workoutPlan = await Task.Run(() => _workoutPlanModel.GenerateWorkoutPlan(workoutType, daysPerWeek));
                WorkoutPlanTextBlock.Text = workoutPlan; // Display the generated workout plan
            }
            catch (System.ArgumentException ex)// handle error of some kind
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SavePlanButton_Click(object sender, RoutedEventArgs e)// save the workout plan to a text file
        {
            var workoutPlan = WorkoutPlanTextBlock.Text;// get the text from the text block

            if (string.IsNullOrWhiteSpace(workoutPlan) || workoutPlan == "Your workout plan will appear here...")// check if the text block is empty or has the default text
            {
                MessageBox.Show("No workout plan to save. Please generate a plan first.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Open a SaveFileDialog to let the user choose where to save the text file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",// only allow text files to be saved
                Title = "Save Workout Plan as Text File"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Save the workout plan on a background thread
                    await Task.Run(() => File.WriteAllText(saveFileDialog.FileName, FormatWorkoutPlanForTextFile(workoutPlan)));
                    MessageBox.Show("Workout plan saved successfully!", "Save Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Failed to save workout plan: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadPlanButton_Click(object sender, RoutedEventArgs e)// load the workout plan from a text file
        {
            //Open an OpenFileDialog to let the user select a .txt file
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Load Workout Plan from Text File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Read the content of the selected file
                    var workoutPlan = File.ReadAllText(openFileDialog.FileName);
                    WorkoutPlanTextBlock.Text = workoutPlan; // Display the loaded workout plan
                    MessageBox.Show("Workout plan loaded successfully!", "Load Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Failed to load workout plan: {ex.Message}", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string FormatWorkoutPlanForTextFile(string workoutPlan)
        {
            // Format the workout plan into a list format
            var lines = workoutPlan.Split('\n');
            var formattedPlan = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.StartsWith("Day"))
                {
                    formattedPlan.AppendLine(line); // Add day header
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    formattedPlan.AppendLine("  - " + line); // Add workout as a list item
                }
            }

            return formattedPlan.ToString();
        }
    }
}
