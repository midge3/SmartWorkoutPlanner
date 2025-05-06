# SmartWorkoutPlanner
This is a Smart Workout Planner for a final project. That will create a workout plan based off a library with workouts.
Author: Collin Wanta

Generating workouts
- workouts are generated based on the type of workout that is slected (powerlifting, bodybuilding, cardio)
- each workout has a set rep range and set range. These are gernerated based on the type of workout that is selected and are random.
- Each workout type will generate a set numhber of workouts per day that is selected
- Cardio workouts are a bit different bcause they generate a 1-2 workouts per day ranging from 30 to 60 minutes and there is a exception case where if workouts in a day are only 1 then the workout will be 60 minutes

Saving Workouts
- workouts are saved to a .txt file in any location the user selects
- These are formated in a list way were it shows the day and each workout that should be done

Loading Workouts
- Loading workouts button will grab a .txt file from the saved workouts file location that the user selects
- it will then display the full workout for the user based on how it was formated or in our case a list

Multi-Threading
- the program uses multi-threading through the async library
- this is done in the load and save workout functions 

Error Hnadling
- the program uses try and except to handle errors
- it will prevent you from generating a blank workout
- it will prevent you from saving a blank workout
- It will prevent you from selecting a wokout type but not selecting a day amount (no workout will be generated)

Workout Types and Workouts
- these are stored in a dictionary that is used to generate the workouts (this is a list)

MVC Model
the program uses the MVC model to separate the logic from the UI
- the model is the workout class that generates the workouts (Workout.cs is the model)
- the view is the UI that displays the workouts and allows the user to interact with the program ( MainWindow.xaml is the view)
- the controller is the main function that runs the program and handles the user input (MainWindow.xaml.cs is the controller)

Inheritance
-The MainWindow class inherits from the Window class, which is part of the WPF framework

Loops
- Loops are used in the FormatWorkoutPlanForTextFile method to iterate through the lines of the workout plan and format them into a list format.
- loops are implicitly used in the WorkoutPlanModel.GenerateWorkoutPlan method to generate the workout plan for the specified number of days.

Methods
- GeneratePlanButton_Click (handles workout plan generation).
•	SavePlanButton_Click (handles saving the workout plan to a .txt file).
•	FormatWorkoutPlanForTextFile (formats the workout plan).
•	Methods in the WorkoutPlanModel class, such as GenerateWorkoutPlan.

Classes
- MainWindow (the main window of the application).
- WorkoutPlanModel (the model that generates the workout plan).
