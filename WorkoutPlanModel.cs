/*
 * Collin Wanta
 * Smart Workout Planner
 * Model
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWorkout_Planner
{
    public class WorkoutPlanModel
    {
        private static readonly Dictionary<string, List<string>> WorkoutLibrary = new()//library of exercises in list format
        {
            { "Powerlifting", new List<string> { "Squats", "Bench Press", "Deadlifts", "Overhead Press", "Barbell Rows" } },
            { "Bodybuilding", new List<string> { "Bicep Curls", "Tricep Extensions", "Leg Press", "Chest Fly", "Lat Pulldown", "Shoulder Press", "Pull-Ups", "Dumbbell Rows", "Incline Bench Press", "Lunges" } },
            { "Cardio", new List<string> { "Running", "Cycling", "Rowing", "Jump Rope", "Swimming" } }
        };

        private static readonly Random Random = new();

        public string GenerateWorkoutPlan(string workoutType, int daysPerWeek)//this is the main generating 
        {
            if (!WorkoutLibrary.ContainsKey(workoutType))
                throw new ArgumentException("Invalid workout type.");// Check if the workout type is valid and has a disctonary entry

            var exercises = WorkoutLibrary[workoutType];//pulls exercises from the dictionary
            var sb = new StringBuilder();//StringBuilder is used to build the workout plan string

            sb.AppendLine($"Workout Type: {workoutType}");//from here on the view is formated in this type of way
            sb.AppendLine($"Days Per Week: {daysPerWeek}");
            sb.AppendLine("\nGenerated Plan:");

            for (int i = 1; i <= daysPerWeek; i++)
            {
                sb.AppendLine($"Day {i}:");
                if (workoutType == "Bodybuilding")//generates a bodybuilding workout
                {
                    GenerateBodybuildingWorkout(sb, exercises);
                }
                else if (workoutType == "Powerlifting")
                {
                    GeneratePowerliftingWorkout(sb, exercises);//generates a powerlifting workout
                }
                else if (workoutType == "Cardio")
                {
                    GenerateCardioWorkout(sb, exercises);//generates a cardio workout
                }
                sb.AppendLine();
            }

            return sb.ToString();//returns the workout plan as a string
        }

        private void GenerateBodybuildingWorkout(StringBuilder sb, List<string> exercises)
        {
            var selectedExercises = GetRandomExercises(exercises, 5); // 5 workouts per day
            foreach (var exercise in selectedExercises)
            {
                int sets = Random.Next(3, 5); // 3 to 4 sets
                int reps = Random.Next(8, 13); // 8 to 12 reps
                sb.AppendLine($"  - {exercise}: {sets} sets of {reps} reps");//formatting view
            }
        }

        private void GeneratePowerliftingWorkout(StringBuilder sb, List<string> exercises)
        {
            var selectedExercises = GetRandomExercises(exercises, Random.Next(3, 5)); // 3 to 4 workouts per day
            foreach (var exercise in selectedExercises)
            {
                int sets = Random.Next(3, 6); // 3 to 5 sets
                int reps = Random.Next(3, 9); // 3 to 8 reps
                sb.AppendLine($"  - {exercise}: {sets} sets of {reps} reps");//formatting view
            }
        }

        private void GenerateCardioWorkout(StringBuilder sb, List<string> exercises)
        {
            int workoutCount = Random.Next(1, 3); // 1 to 2 workouts per day
            var selectedExercises = GetRandomExercises(exercises, workoutCount);

            foreach (var exercise in selectedExercises)
            {
                int duration = workoutCount == 1 ? 60 : Random.Next(30, 61); // 60 minutes if 1 workout, otherwise 30-60 minutes
                sb.AppendLine($"  - {exercise}: {duration} minutes");//formatting view
            }
        }

        private List<string> GetRandomExercises(List<string> exercises, int count)
        {
            var selectedExercises = new List<string>();
            var availableExercises = new List<string>(exercises);

            for (int i = 0; i < count && availableExercises.Count > 0; i++)// checks if there are enough exercises to select
            {
                int index = Random.Next(availableExercises.Count);// selects a random index
                selectedExercises.Add(availableExercises[index]);// adds the exercise to the selected list
                availableExercises.RemoveAt(index); // Remove to avoid duplicates
            }

            return selectedExercises;
        }
    }
}
