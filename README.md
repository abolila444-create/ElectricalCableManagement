# Electrical Cable Management System

A comprehensive Windows Forms application for managing electrical cable installations and tracking road lengths with drum information.

## Features

✅ **User Authentication**
- Secure login form with SQL Server database
- Default credentials: admin/admin123

✅ **Data Management**
- Add, update, delete road length records
- Import/export data to Excel
- Full CRUD operations

✅ **Search Functionality**
- Search by Road Name
- Search by Drum Number
- Search by Work Date

✅ **Reports & Statistics**
- Total number of records
- Total cable lengths
- Number of drums used
- Statistics per road

✅ **Excel Integration**
- Import data directly from Excel
- Export records to Excel format

✅ **Printing**
- Print reports and data

## Project Structure

```
ElectricalCableManagement/
├── Database/
│   ├── DbConnection.cs
│   └── ElectricalCableDB.sql
├── Models/
│   └── RoadLength.cs
├── Utilities/
│   ├── ExcelImport.cs
│   └── ExcelExport.cs
├── LoginForm.cs
├── MainForm.cs
├── SearchForm.cs
├── ReportsForm.cs
├── Program.cs
└── ElectricalCableManagement.csproj
```

## Database Setup

1. Open SQL Server Management Studio
2. Execute the script in `Database/ElectricalCableDB.sql`
3. Verify the ElectricalCableDB database is created

## Getting Started

1. Clone this repository
2. Open the solution in Visual Studio
3. Build the project
4. Run the application
5. Login with admin/admin123

## Database Schema

### Users Table
- UserID (Primary Key)
- UserName
- Password

### RoadLengths Table
- ID (Primary Key)
- SpaceName
- FromLocation
- ToLocation
- StartPoint
- EndPoint
- TotalLength
- DrumNo
- DrumSerial
- RoadName
- MV
- WorkDate
- Remarks

## Connection String

Update the connection string in `Database/DbConnection.cs` if your SQL Server instance differs:

```csharp
Server=.\\SQLEXPRESS;
Database=ElectricalCableDB;
Trusted_Connection=True;
```

## Requirements

- .NET Framework 4.7.2 or higher
- SQL Server 2012 or higher
- Microsoft Office Interop (for Excel functionality)
- Visual Studio 2019 or higher

## Usage

1. **Add Records**: Click "Add" to clear fields, fill in data, and click "Save"
2. **Update Records**: Select a record from the grid, modify fields, and click "Update"
3. **Delete Records**: Select a record and click "Delete"
4. **Search**: Click "Search" to open search dialog with multiple filter options
5. **Export**: Click "Export Excel" to save data as Excel file
6. **Refresh**: Click "Refresh" to reload all data

## Future Enhancements

- Advanced reporting with Crystal Reports
- Data validation and error handling
- Role-based access control
- Audit logging
- Backup and restore functionality
