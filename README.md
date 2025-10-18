# Blazor Notes MVP

A simple, reliable, and secure note-taking application built with ASP.NET Blazor. This Minimal Viable Product (MVP) is designed for a single user on a single device, focusing on core functionality and a clean user experience.

## 🚀 MVP Features (Prioritized)

1.  **Create a New Note:** Quickly create a new note with a title and body.
2.  **View Notes:** See a simple list of all your saved notes.
3.  **Delete a Note:** Permanently remove notes you no longer need.
4.  **Update a Note:** Edit the title and body of existing notes.

## 🗂️ Data Model

Each note contains the following properties:
*   `Id` (Unique Identifier)
*   `Title` (String)
*   `Body` (String)
*   `CreatedAt` (DateTime - Auto-generated)
*   `ModifiedAt` (DateTime - Auto-generated)

## 🛠️ Technology Stack

*   **Framework:** ASP.NET Blazor
*   **Language:** C#
*   **Database:** SQLite (Local file-based persistence)
*   **Architecture:** Repository Pattern

## 📋 Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
*   An IDE (e.g., Visual Studio, VS Code, Rider)

## 🚦 Getting Started

1.  Clone the repository.
2.  Navigate to the project directory in your terminal.
    * Frontend | `cd NoteApp.Web`
    * API | `cd NoteApp.API`
    * Test | `cd NoteApp.Test`
4.  Run `dotnet run` to build and start the application.
5.  Open your browser to the URL provided in the terminal (typically `https://localhost:7000` or `http://localhost:5000`).

The application will automatically create the SQLite database file in your project directory on first run.

## 🔮 Future Considerations

The following features are out of scope for this MVP but are potential candidates for future iterations:
*   User Authentication & Authorization
*   Rich Text Formatting in notes
*   Note Categorization (Tags, Folders)
*   A Card-based UI layout
*   Data Export
