# 📱 Phone Book Management System (Map-Based)

A high-performance Desktop Application built with **C#** and **Windows Forms** that manages contacts efficiently using a **Map (SortedDictionary)** engine. This project demonstrates advanced data structure implementation, JSON persistence, and modern UI coding practices.

---

## 📺 Project Demo & Walkthrough

Check out the full technical breakdown and live demo of the application on YouTube:

[![Phone Book Project Demo](https://img.youtube.com/vi/nyKqG2aFA9A/maxresdefault.jpg)](https://www.youtube.com/watch?v=nyKqG2aFA9A)
*Click the image above to watch the video.*

---

## 🚀 Key Features

- **Lightning-Fast Search:** Uses a `SortedDictionary` to achieve **O(log n)** time complexity for contact retrieval.
- **Data Persistence:** Automatically saves and loads contacts from a `contacts.json` file using **System.Text.Json**.
- **Smart ID System:** Features a robust unique ID generator that prevents duplication even after restarting the app.
- **Data Validation:** Integrated Regex for real-time validation of phone numbers and email addresses.
- **Modern UI:** A clean and responsive interface designed entirely via code (C#) for maximum control.

## 🛠️ Technical Stack

- **Language:** C#
- **Framework:** .NET 10.0 (WinForms)
- **Data Structures:** SortedDictionary (Map), List
- **Serialization:** JSON

## 📂 Project Structure

- `clsContact.cs`: The core data model with unique ID logic.
- `clsPhoneBookEngine.cs`: The business logic layer managing the Map operations.
- `clsFileHelper.cs`: Utility for handling JSON read/write operations.
- `Form1.cs`: The UI layer and event handling.

## ⚙️ How to Run

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Alaa-z17/phone-book-map-dot-net.git](https://github.com/Alaa-z17/phone-book-map-dot-net.git)
Open in Visual Studio:
Load the .sln file.

Build & Run:
Press F5 to start the application.

Alternatively, you can download the ready-to-run version from the Releases section.

🤝 Contributing
Contributions, issues, and feature requests are welcome! Feel free to check the issues page.

Developed by Alaa-z17
