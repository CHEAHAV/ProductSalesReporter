# ProductSalesReporter
This is my project report that make with C# programming and SQL server.

->How to connect project with database

-In my project have Folder name Database Setup that have database file for this project you copy database file name PRODUCTSALE.sql to paste in your SQL server.

-Go to class DatabaseConnection.cs in the project you will see string str = "Data Source=MSI\DBMS;Initial Catalog=DB_PRODUCTSALES;Integrated Security=True"; you must change “Data source = MSI\DBMS; to “Data source = your server name in your SQL Server.

-	In this project must use Dapper package source if you don’t have you must install it.

•	Click right mouse on ProductSalesReporter

•	You will see Manage NuGet Package and click it

•	Click Browse and search Dapper

•	Install Dapper

