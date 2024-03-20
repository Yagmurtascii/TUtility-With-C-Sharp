# TUtility with C Sharp
![1](https://github.com/Yagmurtascii/TUtility-with-C-/assets/64540298/7f3993a9-05ed-4d3e-af9f-7c8efea7bfc4)

## What is the TUtility?

TUtilty is a desktop application used to create procedures.

## What technologies are used?

In this application, C# Window Form, NHibernate(version 5.5.0), Fluent NHibernate(version 3.3.0) and Microsoft.Data.SqlClient(version 5.2.0), MS SQL Server were used.

## Database Connection
Open App.config file and change this section.

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <connectionStrings>
    <add name="MyConnectionString" connectionString="your database connection string" />
  </connectionStrings>
</configuration>
```


