# Data-Structures-Algorithms
Create an interactive console application that allows a user to manage very basic tasks. The user can: 
•	Add tasks 
•	Remove tasks 
•	Update task status (toggle completion) 
•	List tasks 

Additionally, the application can persist data by saving the list of tasks to a JSON file and loading them back when the app starts. We provide a basic implementation walkthrough of a To-Do List application. After completing this walkthrough, a student will be able to expand the project based on the listed requirement

Architecture: We will follow a Clean Architecture approach by separating the application into distinct layers: 
•	Model: Contains the definition of a task.
•	Repository: Handles persistence (saving/loading tasks as JSON).
•	Service: Contains business logic for managing tasks.
•	View: Presents the user interface and interacts with the service.
•	Program: Sets up dependency injection and runs the application.

These are the data-structures that are applied in this console based application: 
- Array
- Singular-LinkedList
- BinarySearchTree
- HashMap

Requirements needed to run the application: 
- At least dotnet version 9.0 or higher 

How to run the application: 
- Navigate to the 'bin/Debug/net9.0/Data-Strucutres-Algorithms.exe' and run this file.