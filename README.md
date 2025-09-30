# FederationSolution

A **GraphQL Federation** project using **HotChocolate** and **Apollo Federation**, backed by **SQL Server**.

This solution demonstrates how to federate multiple subgraphs (Users and Employees) and connect them via Apollo Router.

---

## 🚀 Prerequisites

Before running this solution, make sure you have:

- [Apollo Router](https://www.apollographql.com/docs/router/)
- [Apollo Rover](https://www.apollographql.com/docs/rover/)
- SQL Server (local or remote)

---

## 🗄️ Database Setup

Run the following SQL script to create and seed the required databases:

```sql
-- Create UserDb
CREATE DATABASE UserDb;
GO
USE UserDb;
GO
CREATE TABLE [Users] (
  UserId INT IDENTITY PRIMARY KEY,
  Username NVARCHAR(100) NOT NULL
);
GO
INSERT INTO Users (Username) VALUES ('alice'), ('bob');

-- Create EmployeeDb
CREATE DATABASE EmployeeDb;
GO
USE EmployeeDb;
GO
CREATE TABLE Employee (
  EmployeeId INT IDENTITY PRIMARY KEY,
  UserId INT NOT NULL,
  FirstName NVARCHAR(100),
  LastName NVARCHAR(100)
);
GO
-- Example employee linked to UserId = 1
INSERT INTO Employee (UserId, FirstName, LastName) 
VALUES (1, 'Alice', 'Anderson');
```


⚙️ Install Rover

On Windows PowerShell, run the following to install Rover:
```
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
iwr 'https://rover.apollo.dev/win/latest' | iex
```

▶️ Running the Federation

Start the Subgraphs

Run the UserSubgraph project (connects to UserDb).

Run the EmployeeSubgraph project (connects to EmployeeDb).

⚠️ It is recommended to start both before proceeding.

Run the Router
The Apollo Router binary is already included under EmployeeSubgraph/Router.

Navigate to the router folder:
```
cd EmployeeSubgraph
cd Router
```

Start the router with:
```
rover dev --supergraph-config supergraph-config.yaml
```

Federated Query
```
query {
  getUser(id: "1") {
    id
    username
    fullName
    recommendedEmployee {
      id
      userId
      firstName
      lastName
    }
  }
}
```

