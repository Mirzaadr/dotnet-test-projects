# Palindrome Checker Console App (.NET)

A simple .NET console application that checks if a given string is a **palindrome** (reads the same forwards and backwards).

---

## 📦 Project Structure

```
Palindrome/
│
├── Palindrome.CMD/             # Console application
│ ├── Program.cs                # Entry point
│ └── PalindromeService.cs      # Service that implements checking palindrome logic
│
├── Palindrome.Test/            # xUnit test project
│ └── PalindromeCheckerTest.cs
│
├── Palindrome.sln              # Solution file
└── README.md                   # Project documentation
```

## 🔧 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- Terminal or IDE (e.g., VS Code, Rider, or Visual Studio)

## 🚀 How to Run the App

### ▶️ Option 1: Direct Run

```bash
dotnet run --project Palindrome.CMD
```

This will compile and run the console app.

### ▶️ Option 2: Build and Run Published App

To publish the app (produce a self-contained executable):

```bash
dotnet publish Palindrome.CMD -c Release -o ./publish
```

This creates a `publish` folder with the compiled app.

To run the published app:

On Linux/macOS:

```bash
./publish/Palindrome.CMD
```

On Windows:

```bash
.\publish\Palindrome.CMD.exe
```

You can now distribute and run the app from the `publish` folder without needing the full .NET SDK (only the runtime if not self-contained).

📌 Example Usage

```vbnet
Enter a string to check if it's a palindrome:
A man a plan a canal Panama
true
```

🧪 How to Run the Unit Tests

Ensure you're in the solution root, then run:

```bash
dotnet test
```

🧠 Features

- Accepts user input from the console
- Validates non-empty input
- Checks if a string is a palindrome (case-insensitive, ignores spaces)
- Unit tests included using xUnit
