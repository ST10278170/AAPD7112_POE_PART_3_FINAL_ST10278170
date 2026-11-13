# Municipal Services Application – POE Part 3

A Windows Forms Municipal Services Application developed in C# using .NET Framework 4.7.2.

## Project Overview

The Municipal Services Application provides residents with a way to report municipal issues, explore local events and announcements, and track the progress of submitted service requests.

This project brings together the functionality developed across Parts 1, 2 and 3 of the Programming Application Development POE.

## Features

### Part 1 – Report Issues
- Submit municipal service issues.
- Select an issue category.
- Enter the issue location and description.
- Attach supporting media.
- View and manage reported issues.

### Part 2 – Local Events and Announcements
- Browse upcoming local events.
- Search for events using text entered by the user.
- Filter events by category and date.
- View event details.
- Receive recommendations based on search activity.

### Part 3 – Service Request Status
- View organised service requests.
- Track requests using a unique request ID.
- View request statuses, including Pending, In Progress and Completed.
- Update relevant request details.
- Identify high-priority service requests.
- Explore relationships between requests using graph traversal.

## Data Structures and Algorithms

The application demonstrates the use of the following data structures and algorithms:

- **Binary Search Tree (BST):** Organises service requests and supports searching.
- **AVL Tree:** Maintains a balanced search tree for efficient searching.
- **Red-Black Tree:** Provides another self-balancing search structure.
- **Priority Max-Heap:** Identifies high-priority service requests.
- **Graph:** Represents relationships between service requests.
- **Breadth-First Search (BFS):** Traverses related service requests.
- **Depth-First Search (DFS):** Explores connected service requests.
- **Minimum Spanning Tree (MST):** Uses Kruskal's algorithm to identify a minimum-weight connection structure.

Additional collections, including dictionaries, queues, stacks and sorted dictionaries, are used in the application.

## Technologies and Requirements

- C#
- Windows Forms
- .NET Framework 4.7.2
- Microsoft Visual Studio
- NuGet Package Manager

### NuGet Dependencies

The project uses the following NuGet packages:

- BouncyCastle.Cryptography – Version 2.6.2
- iTextSharp – Version 5.5.13.5

These dependencies are listed in packages.config. Visual Studio can restore them through NuGet Package Restore.

## How to Open and Run the Application

1. Download or clone this repository.
2. Extract the downloaded ZIP file if necessary.
3. Open the project solution file, MunicipalServicesApp.slnx, in a compatible version of Visual Studio.
4. Allow Visual Studio to restore the NuGet packages.
5. Ensure that the .NET Framework 4.7.2 Developer Pack is installed.
6. Build the solution using **Build → Build Solution**.
7. Start the application using **Start** or press **F5**.

If package restoration does not start automatically, use the NuGet package restore option in Visual Studio.

## Using the Application

1. Start the application.
2. Use the main menu to access:
   - Report Issues
   - Local Events and Announcements
   - Service Request Status
3. Follow the instructions on each form to use its functionality.
4. When tracking a service request, enter its unique request ID to locate it.

## Project Structure

The project contains the following main components:

- MainForm.cs – Main application menu.
- ReportIssuesForm.cs – Issue reporting functionality.
- ViewIssuesForm.cs – Displays reported issues.
- EditIssueForm.cs – Allows issue details to be edited.
- LocalEventsForm.cs – Local events and announcements.
- EventRepository.cs – Stores and manages event information.
- ServiceRequestStatusForm.cs – Service request tracking interface.
- ServiceRequestRepository.cs – Manages service requests and their associated data structures.
- ServiceRequestBinarySearchTree.cs – Binary Search Tree implementation.
- ServiceRequestAVLTree.cs – AVL Tree implementation.
- ServiceRequestRedBlackTree.cs – Red-Black Tree implementation.
- ServiceRequestPriorityHeap.cs – Priority Max-Heap implementation.
- ServiceRequestGraph.cs – Graph implementation.
- ServiceRequestMinimumSpanningTree.cs – Minimum Spanning Tree implementation.

## Notes

- The project targets .NET Framework 4.7.2.
- NuGet packages must be restored before building if they are not already available.
- The bin, obj, .vs and packages directories are excluded from version control.
- The source code and project configuration are included in this repository.


**Municipal Services Application – Programming Application Development POE Part 3**
