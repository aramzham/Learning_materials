-- Create Departments table
CREATE TABLE Departments
(
    DepartmentID INTEGER PRIMARY KEY,
    Name         TEXT NOT NULL,
    Location     TEXT,
    Budget       REAL,
    CreatedAt    TEXT,
    IsActive     INTEGER
);

-- Create Employees table with foreign key to Departments
CREATE TABLE Employees
(
    EmployeeID   INTEGER PRIMARY KEY,
    FirstName    TEXT NOT NULL,
    LastName     TEXT NOT NULL,
    Email        TEXT,
    HireDate     TEXT,
    DepartmentID INTEGER,
    FOREIGN KEY (DepartmentID) REFERENCES Departments (DepartmentID)
);

-- Insert dummy data into Departments
INSERT INTO Departments (DepartmentID, Name, Location, Budget, CreatedAt, IsActive)
VALUES (1, 'Engineering', 'Yerevan', 500000, '2023-01-01', 1),
       (2, 'Marketing', 'Gyumri', 200000, '2023-02-01', 1),
       (3, 'Sales', 'Vanadzor', 300000, '2023-03-01', 1),
       (4, 'HR', 'Yerevan', 150000, '2023-04-01', 1),
       (5, 'Finance', 'Gyumri', 400000, '2023-05-01', 1),
       (6, 'Support', 'Vanadzor', 180000, '2023-06-01', 1),
       (7, 'Legal', 'Yerevan', 220000, '2023-07-01', 1),
       (8, 'IT', 'Gyumri', 350000, '2023-08-01', 1),
       (9, 'Operations', 'Vanadzor', 270000, '2023-09-01', 1),
       (10, 'R&D', 'Yerevan', 600000, '2023-10-01', 1);

-- Insert dummy data into Employees
INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, HireDate, DepartmentID)
VALUES (1, 'Anna', 'Petrosyan', 'anna.p@example.com', '2023-01-15', 1),
       (2, 'Gor', 'Sargsyan', 'gor.s@example.com', '2023-02-20', 2),
       (3, 'Lilit', 'Harutyunyan', 'lilit.h@example.com', '2023-03-10', 3),
       (4, 'Arman', 'Grigoryan', 'arman.g@example.com', '2023-04-05', 4),
       (5, 'Nare', 'Avetisyan', 'nare.a@example.com', '2023-05-12', 5),
       (6, 'Karen', 'Hakobyan', 'karen.h@example.com', '2023-06-18', 6),
       (7, 'Mariam', 'Khachatryan', 'mariam.k@example.com', '2023-07-22', 7),
       (8, 'Tigran', 'Vardanyan', 'tigran.v@example.com', '2023-08-30', 8),
       (9, 'Sona', 'Melkonyan', 'sona.m@example.com', '2023-09-14', 9),
       (10, 'Hayk', 'Martirosyan', 'hayk.m@example.com', '2023-10-01', 10);
