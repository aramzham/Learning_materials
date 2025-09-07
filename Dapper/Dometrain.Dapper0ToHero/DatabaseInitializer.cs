using Microsoft.Data.Sqlite;

namespace Dometrain.Dapper0ToHero;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync()
    {
        await using var connection = new SqliteConnection("Data Source=database.db");
        await connection.OpenAsync();

        const string createTables = """
            CREATE TABLE IF NOT EXISTS Departments (
                DepartmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Location TEXT,
                Budget DECIMAL,
                CreatedAt DATETIME,
                IsActive INTEGER
            );

            CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Email TEXT,
                HireDate TEXT,
                DepartmentID INTEGER,
                FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
            );

            INSERT OR IGNORE INTO Departments (DepartmentID, Name, Location, Budget, CreatedAt, IsActive) VALUES
            (1, 'Engineering', 'Building A', 500000, datetime('now'), 1),
            (2, 'Marketing', 'Building B', 200000, datetime('now'), 1);

            INSERT OR IGNORE INTO Employees (EmployeeID, FirstName, LastName, Email, HireDate, DepartmentID) VALUES
            (1, 'John', 'Doe', 'john.doe@company.com', '2023-01-15', 1),
            (2, 'Jane', 'Smith', 'jane.smith@company.com', '2023-02-20', 1),
            (3, 'Bob', 'Johnson', 'bob.johnson@company.com', '2023-03-10', 2);
            """;

        await using var command = new SqliteCommand(createTables, connection);
        await command.ExecuteNonQueryAsync();
    }
}