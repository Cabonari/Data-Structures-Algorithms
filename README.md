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

How to run the tests: 
- Select terminal in vscode. and write in the terminal the following commands: 
1. cd MyTests
2. dotnet test 


# Bijdrage Rushil: 

Gewerkt aan de volgende features: 
- Enforce per-user modification rights 
- Filter or list tasks by priority, status or creation date
- Write test cases to validate the basic functional requirements 

Methodes: 

MyArray.cs: Filter(), Reduce (with and without initial) 
MyLinkedList.cs: Reset, Reduce (with and without initial) 
MybinarySearchTree.cs: GetEnumerator, GetMyIterator, Sort, Reduce (with and without initial)
MyHashMap.cs: Filter, Findby, Remove 


# Bijdrage Jing: 

Gewerkt aan de volgende features:
- Add, remove and toggle tasks (complete task / incomplete task).
- Tasks grouped into columns TODO/InProgress/Review/Done.
- Move tasks across stages to simulate real workflow.
- Assign tasks to team members.
- Allow tasks to depend on others; a task unlocks only when prerequisites are completed.

Methodes:

MyArray.cs: Sort, Reset
MyLinkedList.cs: Sort, GetEnumerator
MybinarySearchTree.cs: Remove
MyHashMap.cs: Add, GetMyIterator, GetEnumerator,


# Bijdrage Jullian: 